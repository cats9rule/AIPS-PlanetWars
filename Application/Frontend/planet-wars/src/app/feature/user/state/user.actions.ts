import { createAction, props } from '@ngrx/store';
import { User } from '../interfaces/user';

export const userLogin = createAction(
  '[Login Page] Login',
  props<{ usernameAndTag: string; password: string }>()
);
export const userLoginSuccess = createAction(
  '[Login Page] Login Success',
  props<{ user: User }>()
);
export const userLoginError = createAction(
  '[Login Page] Login Error',
  props<{ errorMessage: string }>()
);
export const userLogout = createAction('[Global] Logout');
