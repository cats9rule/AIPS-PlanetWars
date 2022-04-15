import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AppState } from 'src/app/core/state/app.state';
import { isDefined } from 'src/app/core/utils/types/maybe.type';
import { Features } from '../../features.enum';
import { UserState } from './user.state';

export const getUserState = createFeatureSelector<UserState>(Features.User);

export const getUserLogged = createSelector(
  getUserState,
  (state: UserState) => state.loggedUser
);

export const getIsUserLogged = createSelector(
  getUserState,
  (state: UserState) => isDefined(state.loggedUser)
);

export const getUserUsernameWithTag = createSelector(
  getUserState,
  (state: UserState) => {
    if (isDefined(state.loggedUser))
      return state.loggedUser?.username + '#' + state.loggedUser?.tag;
    else return null;
  }
);
