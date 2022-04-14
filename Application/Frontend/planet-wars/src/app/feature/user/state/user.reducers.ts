import { createReducer, on } from '@ngrx/store';
import { initialState, UserState } from './user.state';
import * as userActions from './user.actions';

export const userReducer = createReducer(
  initialState,
  on(userActions.userLoginSuccess, (state: UserState, { user }) => ({
    ...state,
    loggedUser: user,
  })),
  on(userActions.userLogout, (state: UserState) => ({
    ...state,
    loggedUser: null,
  }))
);
