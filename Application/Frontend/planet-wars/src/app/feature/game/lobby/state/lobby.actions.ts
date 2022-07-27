import { createAction, props } from '@ngrx/store';
import { CreateGameDto } from '../dtos/createGameDto';
import { JoinGameDto } from '../dtos/joinGameDto';
import { SessionDto } from '../dtos/sessionDto';

export const createGame = createAction(
  '[Lobby] Create Game',
  props<{ createGameDto: CreateGameDto }>()
);

export const createGameSuccess = createAction(
  '[Lobby] Create Game Success',
  props<{ sessionDto: SessionDto }>()
);

export const createGameError = createAction(
  '[Lobby] Create Game Error',
  props<{ errorMessage: string }>()
);

export const joinGame = createAction(
  '[Lobby] Join Game',
  props<{ joinGameDto: JoinGameDto }>()
);
