import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap, switchMap } from 'rxjs';
import { noAction } from 'src/app/core/state/common.actions';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { UserDto } from '../dtos/userDto';
import { User } from '../interfaces/user';
import { LoginService } from '../login/login.service';
import { SignupService } from '../signup/signup.service';
import {
  userLogin,
  userLoginSuccess,
  userSignup,
  userSignupSuccess,
} from './user.actions';

@Injectable()
export class UserEffects {
  constructor(
    private actions$: Actions,
    private loginService: LoginService,
    private snackbarService: SnackbarService,
    private signupService: SignupService
  ) {}

  loginUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(userLogin),
      switchMap((action) => {
        return this.loginService.loginUser(action.userLoginDto).pipe(
          mergeMap((userDto: UserDto) => {
            const user: User = {
              displayedName: userDto.displayedName,
              id: userDto.id,
              tag: userDto.tag,
              username: userDto.username,
            };
            return [userLoginSuccess({ user })];
          })
        );
      })
    )
  );

  loginUserSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(userLoginSuccess),
      mergeMap((action) => {
        this.snackbarService.showMessage(
          {
            type: 'Success',
            contents: `User ${action.user.username}#${action.user.tag} has successfully logged in.`,
          },
          'short'
        );
        return [noAction()];
      })
    )
  );

  signupUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(userSignup),
      switchMap((action) => {
        console.log('login user effect');
        return this.signupService.signupUser(action.userCreateDto).pipe(
          mergeMap((userDto: UserDto) => {
            const user: User = {
              displayedName: userDto.displayedName,
              id: userDto.id,
              tag: userDto.tag,
              username: userDto.username,
            };
            return [userSignupSuccess({ user })];
          })
        );
      })
    )
  );

  signupUserSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(userSignupSuccess),
      mergeMap((action) => {
        this.snackbarService.showMessage(
          {
            type: 'Success',
            contents: `User ${action.user.username}#${action.user.tag} has successfully logged in.`,
          },
          'short'
        );
        return [noAction()];
      })
    )
  );
}
