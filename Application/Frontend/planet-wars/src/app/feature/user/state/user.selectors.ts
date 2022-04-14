//TODO: implement selectors

import { createSelector } from '@ngrx/store';
import { AppState } from 'src/app/core/state/app.state';
import { isDefined } from 'src/app/core/utils/maybe.type';
import { UserState } from './user.state';

export const selectUser = (state: AppState) => state.user;

export const selectUserLogged = createSelector(
  selectUser,
  (state: UserState) => state.loggedUser
);

export const selectUserIsLogged = createSelector(
  selectUser,
  (state: UserState) => isDefined(state.loggedUser)
);

export const selectUserUsernameWithTag = createSelector(
  selectUser,
  (state: UserState) => {
    if (isDefined(state.loggedUser))
      return state.loggedUser?.username + '#' + state.loggedUser?.tag;
    else return null;
  }
);
