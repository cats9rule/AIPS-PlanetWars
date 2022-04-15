import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap, switchMap } from 'rxjs';
import { noAction } from 'src/app/core/state/common.actions';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { UserDto } from '../dtos/userDto';
import { User } from '../interfaces/user';
import { LoginService } from '../login/login.service';
import { userLogin, userLoginSuccess } from './user.actions';

@Injectable()
export class UserEffects {
  constructor(
    private actions$: Actions,
    private loginService: LoginService,
    private snackbarService: SnackbarService
  ) {}

  loginUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(userLogin),
      switchMap((action) => {
        console.log('login user effect');
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
}
