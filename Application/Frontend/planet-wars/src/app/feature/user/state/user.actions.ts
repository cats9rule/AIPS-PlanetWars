import { createAction, props } from '@ngrx/store';
import { UserDto } from '../dtos/userDto';
import { UserLoginDto } from '../dtos/userLoginDto';
import { User } from '../interfaces/user';

export const userLogin = createAction(
  '[Login Page] Login',
  props<{ userLoginDto: UserLoginDto }>()
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
