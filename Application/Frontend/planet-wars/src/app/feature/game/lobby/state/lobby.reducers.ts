import { createReducer, on } from '@ngrx/store';
import { initialLobbyState, LobbyState } from './lobby.state';
import * as lobbyActions from './lobby.actions';

export const lobbyReducer = createReducer(
  initialLobbyState,
  on(
    lobbyActions.loadGameMapsSuccess,
    (state: LobbyState, { gameMapDtos }) => ({
      ...state,
      gameMaps: gameMapDtos,
    })
  )
);
