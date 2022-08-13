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
import { PlanetConnectionInfo } from '../../interfaces/planetConnectionInfo';
import { PlanetRenderInfo } from '../../interfaces/planetRenderInfo';
import { GalaxyConstructorService } from '../../services/galaxy-constructor.service';
import { getGalaxy } from '../../state/session.selectors';
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

  @ViewChild('galaxyMatrix')
  private galaxyMatrix?: ElementRef;

  public planetRenderInfoArray: PlanetRenderInfo[] = [];
  public planetConnectionsArray: PlanetConnectionInfo[] = [];

  public drawGalaxy = false;
  public lines: Line[] = [];

  constructor(
    private store: Store<SessionState>,
    private galaxyConstructor: GalaxyConstructorService
  ) {
    this.galaxy$ = this.store.select<Maybe<GalaxyDto>>(getGalaxy);
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
  }

  ngAfterViewInit(): void {
    const width = this.galaxyMatrix?.nativeElement.clientWidth;
    const height = this.galaxyMatrix?.nativeElement.clientHeight;
    const info = this.galaxyConstructor.constructGalaxy(
      this.galaxy,
      width,
      height
    );
    info.forEach((entry) => {
      this.planetRenderInfoArray = this.planetRenderInfoArray.concat(entry);
    });
    // const lines: Line[] = [];
    // lines.push({ x1: 0, y1: 0, x2: 0, y2: height });

    // const cellHeight = height / this.galaxy!!.gameMap.rows;
    // const cellWidth = width / this.galaxy!!.gameMap.columns;

    // for (let i = 0; i < this.galaxy!!.gameMap.columns; i++) {
    //   lines.push({ x1: cellWidth * i, y1: 0, x2: cellWidth * i, y2: height });
    // }

    // for (let i = 0; i < this.galaxy!!.gameMap.rows; i++) {
    //   lines.push({ x1: 0, y1: cellHeight * i, x2: width, y2: cellHeight * i });
    // }
    // this.lines = lines;

    this.planetConnectionsArray =
      this.galaxyConstructor.getConnectionsRenderInfo();

    this.drawGalaxy = true;
  }

  private generatePlanetRenderInfoArray() {}

  private generatePlanetConnectionsArray() {}

  ngOnDestroy(): void {
    this.galaxySubscription.unsubscribe();
  }
}

interface Line {
  x1: number;
  y1: number;
  x2: number;
  y2: number;
}
