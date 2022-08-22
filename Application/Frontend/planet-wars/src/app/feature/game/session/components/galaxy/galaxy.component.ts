import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { GalaxyDto } from '../../../dtos/galaxyDto';
import { PlayerDto } from '../../../dtos/playerDto';
import { PlanetConnectionInfo } from '../../interfaces/planetConnectionInfo';
import { PlanetRenderInfo } from '../../interfaces/planetRenderInfo';
import { constructGalaxy } from '../../state/session.actions';
import {
  canDrawGalaxy,
  getGalaxy,
  getPlacingArmies,
  getPlanetConnectionsRenderInfo,
  getPlanetsRenderInfo,
  getPlayer,
} from '../../state/session.selectors';
import { SessionState } from '../../state/session.state';
@Component({
  selector: 'app-galaxy',
  templateUrl: './galaxy.component.html',
  styleUrls: ['./galaxy.component.scss'],
})
export class GalaxyComponent implements OnInit, OnDestroy, AfterViewInit {
  public galaxy$: Observable<Maybe<GalaxyDto>>;
  public galaxy: Maybe<GalaxyDto>;
  private galaxySubscription: Subscription = new Subscription();

  private placingArmies$: Observable<boolean>;
  private placingArmies: boolean = false;
  private placingArmiesSubscription = new Subscription();

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
    this.placingArmies$ = this.store.select<boolean>(getPlacingArmies);
    this.player$ = this.store.select<Maybe<PlayerDto>>(getPlayer);
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

    this.placingArmiesSubscription = this.placingArmies$.subscribe({
      next: (placingArmies) => {
        this.placingArmies = placingArmies;
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
        console.log('Render info sub from Galaxy Component');
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
    const planet = this.planetRenderInfoArray.find(
      (p) => p.indexInGalaxy == index
    );
    if (
      planet != undefined &&
      this.placingArmies &&
      planet.strokeColor == this.player?.playerColor
    ) {
      alert('Placing armies here');
    } else alert('i was clicked: ' + index);
    event?.preventDefault();
  }

  ngOnDestroy(): void {
    this.galaxySubscription.unsubscribe();
    this.planetsRenderinfoSubscription.unsubscribe();
    this.placingArmiesSubscription.unsubscribe();
    this.playerSubscription.unsubscribe();
  }
}
