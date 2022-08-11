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
    this.galaxyConstructor.constructGalaxy(this.galaxy, width, height);
  }

  private generatePlanetRenderInfoArray() {}

  private generatePlanetConnectionsArray() {}

  ngOnDestroy(): void {
    this.galaxySubscription.unsubscribe();
  }
}
