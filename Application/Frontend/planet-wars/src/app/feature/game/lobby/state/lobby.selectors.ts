import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Features } from 'src/app/feature/features.enum';
import { LobbyState } from './lobby.state';

export const getLobbyState = createFeatureSelector<LobbyState>(Features.Lobby);

export const getGameMaps = createSelector(
  getLobbyState,
  (state: LobbyState) => state.gameMaps
);

//TODO: razmisli o tome koji selektor treba da uzima ovo
