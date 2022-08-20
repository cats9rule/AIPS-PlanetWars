import { createFeatureSelector, createSelector } from '@ngrx/store';
import { isDefined } from 'src/app/core/utils/types/maybe.type';
import { Features } from 'src/app/feature/features.enum';
import { SessionState } from './session.state';

export const getSessionState = createFeatureSelector<SessionState>(
  Features.Session
);

export const getSession = createSelector(
  getSessionState,
  (state: SessionState) => state.session
);

export const getIsInSession = createSelector(
  getSessionState,
  (state: SessionState) => {
    console.log(state);
    if (isDefined(state.session)) return true;
    else return false;
  }
);

export const getGalaxy = createSelector(
  getSessionState,
  (state: SessionState) => state.session?.galaxy
);

export const getPlanets = createSelector(
  getSessionState,
  (state: SessionState) => state.planets
);

export const getPlanetsRenderInfo = createSelector(
  getSessionState,
  (state: SessionState) => state.planetsRenderInfo
);

export const getPlanetConnectionsRenderInfo = createSelector(
  getSessionState,
  (state: SessionState) => state.planetConnectionsInfo
);

export const canDrawGalaxy = createSelector(
  getSessionState,
  (state: SessionState) => state.planetConnectionsInfo.length != 0
);

export const getPlayer = createSelector(
  getSessionState,
  (state: SessionState) => state.player
);

export const getAllPlayers = createSelector(
  getSessionState,
  (state: SessionState) => state.session?.players
);
