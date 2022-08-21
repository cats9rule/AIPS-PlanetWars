import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { concatMap, mergeMap } from 'rxjs';
import { noAction } from 'src/app/core/state/common.actions';
import { MessageHubService } from '../../services/message-hub.service';
import { GalaxyConstructorService } from '../services/galaxy-constructor.service';
import {
  addNewPlayer,
  constructGalaxy,
  constructGalaxySuccess,
  constructPlanetConnectionsRenderInfo,
  constructPlanetConnectionsRenderInfoSuccess,
  constructPlanetRenderInfo,
  constructPlanetRenderInfoSuccess,
  joinSessionGroup,
  updatePlanet,
  updatePlanetOwner,
} from './session.actions';

@Injectable()
export class SessionEffects {
  constructor(
    private actions$: Actions,
    private galaxyConstructor: GalaxyConstructorService,
    private hubService: MessageHubService
  ) {}

  joinSessionGroup$ = createEffect(() =>
    this.actions$.pipe(
      ofType(joinSessionGroup),
      mergeMap((action) => {
        this.hubService.startHubConnection(action.user, action.sessionID);
        return [noAction()];
      })
    )
  );

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
          console.log(planetsRenderInfo);
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

  addNewPlayer$ = createEffect(() => 
      this.actions$.pipe(
        ofType(addNewPlayer),
        mergeMap((action) => {
          console.log(action.playerDto.planets[0]);
          const planet = this.galaxyConstructor.createPlanetFromDto(action.playerDto.planets[0]);
          return [updatePlanet({planet})]
        })
      )
  )

  updatePlanet$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updatePlanet),
      mergeMap(() => {
        return [constructPlanetRenderInfo()];
      })
    )
  );
}
