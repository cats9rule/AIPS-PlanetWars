import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, mergeMap, switchMap } from 'rxjs';
import { noAction } from 'src/app/core/state/common.actions';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { GameMap } from '../../interfaces/gameMap';
import { GameMapService } from '../services/game-map.service';
import {
  loadGameMaps,
  loadGameMapsError,
  loadGameMapsSuccess,
} from './lobby.actions';

@Injectable()
export class LobbyEffects {
  constructor(
    private actions$: Actions,
    private gameMapService: GameMapService
  ) {}

  getGameMaps$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadGameMaps),
      switchMap((action) => {
        console.log('GAME MAP LOADING EFFECT');
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
}
