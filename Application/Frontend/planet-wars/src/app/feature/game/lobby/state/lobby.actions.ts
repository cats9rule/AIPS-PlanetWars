import { createAction, props } from '@ngrx/store';
import { Galaxy } from '../../interfaces/galaxy';
import { GameMap } from '../../interfaces/gameMap';
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

export const joinGameSuccess = createAction(
  '[Lobby] Join Game Success',
  props<{ sessionDto: SessionDto }>()
);

export const joinGameError = createAction(
  '[Lobby] Join Game Error',
  props<{ errorMessage: string }>()
);

export const loadGalaxy = createAction(
  '[Lobby] Load Galaxy',
  props<{ galaxyID: string }>()
);

export const loadGalaxySuccess = createAction(
  '[Lobby] Load Galaxy Success',
  props<{ galaxyDto: Galaxy }>
);

export const loadGalaxyError = createAction(
  '[Lobby] Load Galaxy Error',
  props<{ errorMessage: string }>()
);

export const loadGameMap = createAction(
  '[Lobby] Load Game Map',
  props<{gameMapID: string}>()
);

export const loadGameMapSuccess = createAction(
  '[Lobby] Load Game Map Success',
  props<{ gameMapDto: GameMap }>()
);

export const loadGameMapError = createAction(
  '[Lobby] Load Game Map Error',
  props<{ errorMessage: string }>()
);

export const loadGameMaps = createAction(
  '[Lobby] Load Game Maps'
);

export const loadGameMapsSuccess = createAction(
  '[Lobby] Load Game Maps Success',
  props<{ gameMapDtos: GameMap[] }>()
);

export const loadGameMapsError = createAction(
  '[Lobby] Load Game Maps Error',
  props<{ errorMessage: string }>()
);