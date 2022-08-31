import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { ActionType } from 'core/enums/actionType.enum';
import { TurnActionDialogData } from 'core/interfaces/turn-action-dialog-data';
import { openActionDialog } from 'core/state/dialog.actions';
import { Observable, Subscription } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { GalaxyDto } from '../../../dtos/galaxyDto';
import { PlayerDto } from '../../../dtos/playerDto';
import { Planet } from '../../interfaces/planet';
import { PlanetConnectionInfo } from '../../interfaces/planetConnectionInfo';
import { PlanetRenderInfo } from '../../interfaces/planetRenderInfo';
import { SessionService } from '../../services/session.service';
import { constructGalaxy, setRenderWindow } from '../../state/session.actions';
import {
  canDrawGalaxy,
  getGalaxy,
  getPlanetConnectionsRenderInfo,
  getPlanetsRenderInfo,
  getPlayer,
  getSessionState,
} from '../../state/session.selectors';
import { initialSessionState, SessionState } from '../../state/session.state';
@Component({
  selector: 'app-galaxy',
  templateUrl: './galaxy.component.html',
  styleUrls: ['./galaxy.component.scss'],
})
export class GalaxyComponent implements OnInit, OnDestroy, AfterViewInit {
  // @Input()
  public placingArmies = false;
  // @Input()
  public movingArmies = false;
  // @Input()
  public attacking = false;

  private startPlanet: Planet | null = null;

  //#region Observables
  public sessionState$: Observable<SessionState>;
  public sessionState: SessionState = initialSessionState;
  private sessionStateSubscription: Subscription = new Subscription();

  public galaxy$: Observable<Maybe<GalaxyDto>>;
  public galaxy: Maybe<GalaxyDto>;
  private galaxySubscription: Subscription = new Subscription();

  private player$: Observable<Maybe<PlayerDto>>;
  private player: Maybe<PlayerDto>;
  private playerSubscription = new Subscription();

  public planetsRenderInfo$: Observable<PlanetRenderInfo[]>;
  private planetsRenderinfoSubscription: Subscription = new Subscription();

  public planetConnectionsRenderInfo$: Observable<PlanetConnectionInfo[]>;
  public canDrawGalaxy$: Observable<boolean>;

  private actionMessageSub = new Subscription();
  //#endregion

  @ViewChild('galaxyMatrix')
  private galaxyMatrix?: ElementRef;

  public planetRenderInfoArray: PlanetRenderInfo[] = [];
  public planetConnectionsArray: PlanetConnectionInfo[] = [];
  public drawGalaxy = false;

  constructor(
    private store: Store<SessionState>,
    private cdRef: ChangeDetectorRef,
    private sessionService: SessionService
  ) {
    this.galaxy$ = this.store.select<Maybe<GalaxyDto>>(getGalaxy);
    this.planetsRenderInfo$ =
      this.store.select<PlanetRenderInfo[]>(getPlanetsRenderInfo);
    this.planetConnectionsRenderInfo$ = this.store.select<
      PlanetConnectionInfo[]
    >(getPlanetConnectionsRenderInfo);

    this.canDrawGalaxy$ = this.store.select<boolean>(canDrawGalaxy);
    this.player$ = this.store.select<Maybe<PlayerDto>>(getPlayer);

    this.sessionState$ = this.store.select<SessionState>(getSessionState);
  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  ngAfterViewInit(): void {
    const width = this.galaxyMatrix?.nativeElement.clientWidth;
    const height = this.galaxyMatrix?.nativeElement.clientHeight;

    this.store.dispatch(setRenderWindow({ height, width }));

    if (isDefined(this.galaxy)) {
      this.store.dispatch(
        constructGalaxy({
          galaxyDto: this.galaxy!!,
          matrixWidth: width,
          matrixHeight: height,
        })
      );
    }

    this.cdRef.detectChanges();
  }

  public onPlanetClick(index: number) {
    const planet = this.sessionState.planets.find(
      (p) => p.getIndexInGalaxy() == index
    );

    if (planet == undefined) return;

    this.resolveClickAction(planet);

    // if (
    //   planet != undefined &&
    //   this.isPlacingArmies &&
    //   planet.getOwnerID() == this.player?.id
    // ) {
    //   this.openPlacementActionDialog(planet);
    // } else alert('i was clicked: ' + index);
    event?.preventDefault();
  }

  public getArmyCount(index: number) {
    const planet = this.sessionState.planets.find(
      (p) => p.getIndexInGalaxy() == index
    );
    if (planet != undefined) {
      return planet.getArmyCount();
    }
    return 0;
  }

  private initSubscriptions() {
    this.galaxySubscription = this.galaxy$.subscribe({
      next: (galaxy) => {
        if (isDefined(galaxy)) {
          this.galaxy = galaxy;
        }
      },
      error: (err) => {
        console.error(err);
      },
    });

    this.playerSubscription = this.player$.subscribe({
      next: (p) => {
        if (isDefined(p)) {
          this.player = p;
        }
      },
      error: (err) => {
        console.error(err);
      },
    });

    this.planetsRenderinfoSubscription = this.planetsRenderInfo$.subscribe({
      next: (renderInfoMap) => {
        let renderInfos: PlanetRenderInfo[] = [];
        renderInfoMap.forEach((value) => {
          renderInfos = renderInfos.concat(value);
        });
        this.planetRenderInfoArray = renderInfos;
      },
      error: (err) => {
        console.error(err);
      },
    });

    this.sessionStateSubscription = this.sessionState$.subscribe({
      next: (state) => {
        this.sessionState = state;
      },
      error: (err) => console.error(err),
    });

    this.actionMessageSub = this.sessionService.messageSource.subscribe(
      (message) => {
        console.log('Message: ', message);
        switch (message) {
          case 0: {
            this.setPlacingArmies(true);
            break;
          }
          case 1: {
            this.setMovingArmies(true);
            break;
          }
          case 2: {
            this.setAttacking(true);
            break;
          }
          case 3: {
            // total reset
            this.resetAll();
            break;
          }
          default: {
            this.setPlacingArmies(false);
            break;
          }
        }
      }
    );
  }

  private resolveClickAction(planet: Planet) {
    if (this.placingArmies && planet.getOwnerID() == this.player?.id) {
      this.openPlacementActionDialog(planet);
    } else if (this.movingArmies && planet.getOwnerID() == this.player?.id) {
      if (this.startPlanet == null) {
        this.startPlanet = planet;
      } else {
        const connectedPlanets = new Set<number>(
          this.sessionState.session?.galaxy.gameMap.planetGraph[
            `${this.startPlanet.getIndexInGalaxy()}`
          ]
        );

        if (this.startPlanet.getMovement() == 2) {
          const moreConnections = new Set<number>();
          for (let current of connectedPlanets) {
            const c: number[] =
              this.sessionState.session?.galaxy.gameMap.planetGraph[
                `${current}`
              ];
            c.forEach((num) => {
              moreConnections.add(num);
            });
          }
          for (let conn of moreConnections) {
            connectedPlanets.add(conn);
          }
        }

        if (connectedPlanets.has(planet.getIndexInGalaxy())) {
          this.openMovementActionDialog(planet);
        }
      }
    } else if (this.attacking) {
      if (this.startPlanet == null && planet.getOwnerID() == this.player?.id) {
        console.log('Start planet set: ' + planet.getIndexInGalaxy());
        this.startPlanet = planet;
      } else if (
        this.startPlanet != null &&
        planet.getOwnerID() != this.player?.id
      ) {
        const connectedPlanets = new Set<number>(
          this.sessionState.session?.galaxy.gameMap.planetGraph[
            `${this.startPlanet.getIndexInGalaxy()}`
          ]
        );
        console.log(connectedPlanets);
        if (connectedPlanets.has(planet.getIndexInGalaxy())) {
          this.openAttackActionDialog(planet);
        }
      }
    }
  }

  private openPlacementActionDialog(planet: Planet) {
    const data: TurnActionDialogData = {
      action: ActionType.Placement,
      availableArmies: this.sessionState.armiesToPlace,
      planetIDs: [planet.getID()],
    };
    this.store.dispatch(openActionDialog({ data }));
  }

  private openMovementActionDialog(planet: Planet) {
    const data: TurnActionDialogData = {
      action: ActionType.Movement,
      availableArmies: this.startPlanet!!.getArmyCount(),
      planetIDs: [this.startPlanet!!.getID(), planet.getID()],
    };
    this.store.dispatch(openActionDialog({ data }));
  }

  private openAttackActionDialog(planet: Planet) {
    const data: TurnActionDialogData = {
      action: ActionType.Attack,
      availableArmies: this.startPlanet!!.getArmyCount(),
      planetIDs: [this.startPlanet!!.getID(), planet.getID()],
    };
    this.store.dispatch(openActionDialog({ data }));
  }

  private setPlacingArmies(isPlacing: boolean) {
    this.placingArmies = isPlacing;
    this.movingArmies = false;
    this.attacking = false;
    this.startPlanet = null;
  }

  private setMovingArmies(isMoving: boolean) {
    this.movingArmies = isMoving;
    this.attacking = false;
    this.placingArmies = false;
    this.startPlanet = null;
  }

  private setAttacking(isAttacking: boolean) {
    console.log(isAttacking);
    this.attacking = isAttacking;
    this.movingArmies = false;
    this.placingArmies = false;
    this.startPlanet = null;
  }

  private resetAll() {
    this.attacking = false;
    this.movingArmies = false;
    this.placingArmies = false;
    this.startPlanet = null;
  }

  ngOnDestroy(): void {
    this.galaxySubscription.unsubscribe();
    this.planetsRenderinfoSubscription.unsubscribe();
    this.playerSubscription.unsubscribe();
    this.sessionStateSubscription.unsubscribe();
  }
}
