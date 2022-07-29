import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, mergeMap, switchMap } from 'rxjs';
import { noAction } from 'src/app/core/state/common.actions';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { GameMap } from '../../interfaces/gameMap';
import { SessionDto } from '../dtos/sessionDto';
import { GameMapService } from '../services/game-map.service';
import { InitGameService } from '../services/init-game.service';
import {
  createGame,
  createGameError,
  createGameSuccess,
  joinGame,
  joinGameSuccess,
  loadGameMaps,
  loadGameMapsError,
  loadGameMapsSuccess,
} from './lobby.actions';

@Injectable()
export class LobbyEffects {
  constructor(
    private actions$: Actions,
    private gameMapService: GameMapService,
    private initGameService: InitGameService
  ) {}

  getGameMaps$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadGameMaps),
      switchMap((action) => {
        return this.gameMapService.loadGameMaps().pipe(
          mergeMap((gameMapDtos: GameMap[]) => {
            return [loadGameMapsSuccess({ gameMapDtos })];
          }),
          catchError((error) => {
            return [loadGameMapsError(error)];
          })
        );
      })
    )
  );

  createGame$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createGame),
      switchMap((action) => {
        return this.initGameService.createGame(action.createGameDto).pipe(
          mergeMap((sessionDto: SessionDto) => [
            createGameSuccess({ sessionDto }),
          ]),
          catchError((error) => [createGameError(error)])
        );
      })
    )
  );

  createGameSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createGameSuccess),
      switchMap((action) => {
        //TODO: open game screen
        alert(
          `Game has been created. Name: ${action.sessionDto.name}; Game Code: ${action.sessionDto.gameCode}`
        );
        return [noAction()];
      })
    )
  );

  joinGame$ = createEffect(() =>
    this.actions$.pipe(
      ofType(joinGame),
      switchMap((action) => {
        return this.initGameService.joinGame(action.joinGameDto).pipe(
          mergeMap((sessionDto: SessionDto) => [
            joinGameSuccess({ sessionDto }),
          ]),
          catchError((error) => [createGameError(error)])
        );
      })
    )
  );

  joinGameSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(joinGameSuccess),
      switchMap((action) => {
        //TODO: open game screen
        alert(
          `Game has been joined. Name: ${action.sessionDto.name}; Game Code: ${action.sessionDto.gameCode}`
        );
        return [noAction()];
      })
    )
  );
}
