import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { catchError, mergeMap, switchMap, withLatestFrom } from 'rxjs';
import { noAction } from 'src/app/core/state/common.actions';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { getUserLogged } from 'src/app/feature/user/state/user.selectors';
import { UserState } from 'src/app/feature/user/state/user.state';
import { GameMapDto } from '../../dtos/gameMapDto';
import { SessionDto } from '../../dtos/sessionDto';
import { joinSessionGroup } from '../../session/state/session.actions';
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
    private initGameService: InitGameService,
    private userStore: Store<UserState>
  ) {}

  getGameMaps$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadGameMaps),
      switchMap((action) => {
        return this.gameMapService.loadGameMaps().pipe(
          mergeMap((gameMapDtos: GameMapDto[]) => {
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
      withLatestFrom(this.userStore.select(getUserLogged)),
      switchMap((action) => {
        return this.initGameService.createGame(action[0].createGameDto).pipe(
          mergeMap((sessionDto: SessionDto) => [
            createGameSuccess({ sessionDto, userID: action[1]!!.id }),
          ]),
          catchError((error) => [createGameError(error)])
        );
      })
    )
  );

  createGameSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createGameSuccess),
      withLatestFrom(this.userStore.select(getUserLogged)),
      switchMap((action) => {
        alert(
          `Game has been created. Name: ${action[0].sessionDto.name}; Game Code: ${action[0].sessionDto.gameCode}`
        );
        return [
          joinSessionGroup({
            user: action[1],
            sessionID: action[0].sessionDto.id,
          }),
        ];
      })
    )
  );

  joinGame$ = createEffect(() =>
    this.actions$.pipe(
      ofType(joinGame),
      withLatestFrom(this.userStore.select(getUserLogged)),
      switchMap((action) => {
        return this.initGameService.joinGame(action[0].joinGameDto).pipe(
          mergeMap((sessionDto: SessionDto) => [
            joinGameSuccess({ sessionDto, userID: action[1]!!.id }),
          ]),
          catchError((error) => [createGameError(error)])
        );
      })
    )
  );

  joinGameSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(joinGameSuccess),
      withLatestFrom(this.userStore.select(getUserLogged)),
      switchMap((action) => {
        alert(
          `Game has been joined. Name: ${action[0].sessionDto.name}; Game Code: ${action[0].sessionDto.gameCode}`
        );
        return [
          joinSessionGroup({
            user: action[1],
            sessionID: action[0].sessionDto.id,
          }),
        ];
      })
    )
  );
}
