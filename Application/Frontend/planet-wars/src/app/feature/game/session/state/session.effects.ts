import { Injectable } from '@angular/core';
import { act, Actions, createEffect, ofType } from '@ngrx/effects';
import { ActionType } from 'core/enums/actionType.enum';
import { TurnActionDialogResult } from 'core/interfaces/turn-action-dialog-result';
import { concatMap, mergeMap, tap } from 'rxjs';
import {
  noAction,
  setTurnActionDialogResult,
} from 'src/app/core/state/common.actions';
import { MessageHubService } from '../../services/message-hub.service';
import { GalaxyConstructorService } from '../services/galaxy-constructor.service';
import { TurnBuilderService } from '../services/turn-builder.service';
import {
  addNewPlayer,
  constructGalaxy,
  constructGalaxySuccess,
  constructPlanetConnectionsRenderInfo,
  constructPlanetConnectionsRenderInfoSuccess,
  constructPlanetRenderInfo,
  constructPlanetRenderInfoSuccess,
  joinSessionGroup,
  setSessionState,
  updatePlanet,
  updatePlanetOwner,
} from './session.actions';

@Injectable()
export class SessionEffects {
  constructor(
    private actions$: Actions,
    private galaxyConstructor: GalaxyConstructorService,
    private hubService: MessageHubService,
    private turnBuilder: TurnBuilderService
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
        const planet = this.galaxyConstructor.createPlanetFromDto(
          action.playerDto.planets[0]
        );
        return [updatePlanet({ planet })];
      })
    )
  );

  updatePlanet$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updatePlanet),
      mergeMap(() => {
        return [constructPlanetRenderInfo()];
      })
    )
  );

  setTurnActionDialogResult$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(setTurnActionDialogResult),
        tap((action) => {
          this.resolveTurnAction(action.result);
        })
      ),
    { dispatch: false }
  );

  // setSessionState$ = createEffect(() =>
  //   this.actions$.pipe(
  //     ofType(setSessionState),
  //     mergeMap((action) => {
  //       return [constructPlanetRenderInfo()];
  //     })
  //   )
  // );

  private resolveTurnAction(result: TurnActionDialogResult) {
    switch (result.actionType) {
      case ActionType.Placement: {
        this.turnBuilder.addPlacementAction(result.planetID, result.armyCount);
      }
    }
  }
}
