import { createAction, props } from '@ngrx/store';
import { GameMapDto } from '../../dtos/gameMapDto';
import { CreateGameDto } from '../dtos/createGameDto';
import { JoinGameDto } from '../dtos/joinGameDto';
import { SessionDto } from '../../dtos/sessionDto';

export const createGame = createAction(
  '[Lobby] Create Game',
  props<{ createGameDto: CreateGameDto }>()
);

export const createGameSuccess = createAction(
  '[Lobby] Create Game Success',
  props<{ sessionDto: SessionDto; userID: string }>()
);

export const createGameError = createAction(
  '[Lobby] Create Game Error',
  props<{ errorMessage: string }>()
);

export const joinGame = createAction(
  '[Lobby] Join Game',
  props<{ joinGameDto: JoinGameDto }>()
);

export const joinGameSuccess = createAction(
  '[Lobby] Join Game Success',
  props<{ sessionDto: SessionDto; userID: string }>()
);

export const joinGameError = createAction(
  '[Lobby] Join Game Error',
  props<{ errorMessage: string }>()
);

export const loadGameMaps = createAction('[Lobby] Load Game Maps');

export const loadGameMapsSuccess = createAction(
  '[Lobby] Load Game Maps Success',
  props<{ gameMapDtos: GameMapDto[] }>()
);

export const loadGameMapsError = createAction(
  '[Lobby] Load Game Maps Error',
  props<{ errorMessage: string }>()
);
