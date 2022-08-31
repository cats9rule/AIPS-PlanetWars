import { Injectable } from '@angular/core';
import { act, Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { ActionType } from 'core/enums/actionType.enum';
import { TurnActionDialogResult } from 'core/interfaces/turn-action-dialog-result';
import { concatMap, map, mergeMap, tap, withLatestFrom } from 'rxjs';
import {
  noAction,
  setTurnActionDialogResult,
} from 'src/app/core/state/common.actions';
import { MessageHubService } from '../../services/message-hub.service';
import { GalaxyConstructorService } from '../services/galaxy-constructor.service';
import { SessionService } from '../services/session.service';
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
  playMove,
  setSessionState,
  updatePlanet,
  updatePlanetOwner,
  updateSession,
} from './session.actions';
import { getSessionState } from './session.selectors';
import { SessionState } from './session.state';

@Injectable()
export class SessionEffects {
  constructor(
    private actions$: Actions,
    private galaxyConstructor: GalaxyConstructorService,
    private hubService: MessageHubService,
    private turnBuilder: TurnBuilderService,
    private sessionService: SessionService,
    private store: Store<SessionState>
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

  constructPlanetConnectionsRenderInfo$ = createEffect(() =>
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

  constructPlanetConnectionsRenderInfoSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(constructPlanetConnectionsRenderInfoSuccess),
        withLatestFrom(this.store.select(getSessionState)),
        tap((action) => {
          this.turnBuilder.newTurn(action[1]);
        })
      ),
    { dispatch: false }
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
          if (action.result.armyCount > 0) {
          }
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

  updateSession$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateSession),
      withLatestFrom(this.store.select(getSessionState)),
      mergeMap((action) => {
        return [
          constructGalaxy({
            galaxyDto: action[0].gameUpdate.session.galaxy,
            matrixHeight: action[1].renderHeight,
            matrixWidth: action[1].renderWidth,
          }),
        ];
      })
    )
  );

  playMove$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(playMove),
        tap((action) => {
          this.sessionService.playMove(action.turnDto);
        })
      ),
    { dispatch: false }
  );

  private resolveTurnAction(result: TurnActionDialogResult) {
    switch (result.actionType) {
      case ActionType.Placement: {
        this.turnBuilder.addPlacementAction(
          result.planetIDs[0],
          result.armyCount
        );
        break;
      }
      case ActionType.Movement: {
        this.turnBuilder.addMovementAction(
          result.planetIDs[0],
          result.planetIDs[1],
          result.armyCount
        );
        break;
      }
      case ActionType.Attack: {
        this.turnBuilder.addAttackAction(
          result.planetIDs[0],
          result.planetIDs[1],
          result.armyCount
        );
        break;
      }
    }
  }
}
