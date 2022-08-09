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
