import { createAction, props } from '@ngrx/store';
import { UserCreateDto } from '../dtos/userCreateDto';
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

export const userSignup = createAction(
  '[Signup Page] Signup',
  props<{ userCreateDto: UserCreateDto }>()
);
export const userSignupSuccess = createAction(
  '[Signup Page] Signup Success',
  props<{ user: User }>()
);
export const userSignupError = createAction(
  '[Signup Page] Signup Error',
  props<{ errorMessage: string }>()
);