import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnDestroy,
  OnInit,
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
import { constructGalaxy } from '../../state/session.actions';
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
  @Input()
  public isPlacingArmies = false;

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

  @ViewChild('galaxyMatrix')
  private galaxyMatrix?: ElementRef;

  public planetRenderInfoArray: PlanetRenderInfo[] = [];
  public planetConnectionsArray: PlanetConnectionInfo[] = [];
  public drawGalaxy = false;

  constructor(private store: Store<SessionState>) {
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
  }

  ngAfterViewInit(): void {
    const width = this.galaxyMatrix?.nativeElement.clientWidth;
    const height = this.galaxyMatrix?.nativeElement.clientHeight;
    if (isDefined(this.galaxy)) {
      this.store.dispatch(
        constructGalaxy({
          galaxyDto: this.galaxy!!,
          matrixWidth: width,
          matrixHeight: height,
        })
      );
    }
  }

  public onPlanetClick(index: number) {
    const planet = this.sessionState.planets.find(
      (p) => p.getIndexInGalaxy() == index
    );
    if (
      planet != undefined &&
      this.isPlacingArmies &&
      planet.getOwnerID() == this.player?.id
    ) {
      this.openPlacementActionDialog(planet);
    } else alert('i was clicked: ' + index);
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

  private openPlacementActionDialog(planet: Planet) {
    const data: TurnActionDialogData = {
      action: ActionType.Placement,
      availableArmies: this.sessionState.armiesToPlace,
      planetID: planet.getID(),
    };
    this.store.dispatch(openActionDialog({ data }));
  }

  ngOnDestroy(): void {
    this.galaxySubscription.unsubscribe();
    this.planetsRenderinfoSubscription.unsubscribe();
    //this.placingArmiesSubscription.unsubscribe();
    this.playerSubscription.unsubscribe();
    this.sessionStateSubscription.unsubscribe();
  }
}
