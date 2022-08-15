import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap } from 'rxjs';
import { GalaxyConstructorService } from '../services/galaxy-constructor.service';
import {
  constructGalaxy,
  constructGalaxySuccess,
  constructPlanetConnectionsRenderInfo,
  constructPlanetConnectionsRenderInfoSuccess,
  constructPlanetRenderInfo,
  constructPlanetRenderInfoSuccess,
  updatePlanetOwner,
} from './session.actions';

@Injectable()
export class SessionEffects {
  constructor(
    private actions$: Actions,
    private galaxyConstructor: GalaxyConstructorService
  ) {}

  constructGalaxy$ = createEffect(() =>
    this.actions$.pipe(
      ofType(constructGalaxy),
      mergeMap((action) => {
        const planets = this.galaxyConstructor.constructGalaxy(
          action.galaxyDto,
          action.matrixWidth,
          action.matrixHeight
        );
        return [
          constructGalaxySuccess({ planets }),
          constructPlanetRenderInfo(),
        ];
      })
    )
  );

  constructPlanetRenderInfo$ = createEffect(() =>
    this.actions$.pipe(
      ofType(constructPlanetRenderInfo),
      mergeMap(() => {
        const planetsRenderInfo =
          this.galaxyConstructor.getRenderInfoForGalaxy();
        return [
          constructPlanetRenderInfoSuccess({ planetsRenderInfo }),
          constructPlanetConnectionsRenderInfo(),
        ];
      })
    )
  );

  constructPlanetConnectionsRenderInfo = createEffect(() =>
    this.actions$.pipe(
      ofType(constructPlanetConnectionsRenderInfo),
      mergeMap(() => {
        const connectionsRenderInfo =
          this.galaxyConstructor.getConnectionsRenderInfo();
        return [
          constructPlanetConnectionsRenderInfoSuccess({
            connectionsRenderInfo,
          }),
        ];
      })
    )
  );

  updatePlanetOwner$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updatePlanetOwner),
      mergeMap(() => {
        return [constructPlanetRenderInfo()];
      })
    )
  );
}
