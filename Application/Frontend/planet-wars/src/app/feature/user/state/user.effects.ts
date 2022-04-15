import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap, switchMap } from 'rxjs';
import { UserDto } from '../dtos/userDto';
import { User } from '../interfaces/user';
import { LoginService } from '../login/login.service';
import { userLogin, userLoginSuccess } from './user.actions';

@Injectable()
export class UserEffects {
  constructor(private actions$: Actions, private loginService: LoginService) {}

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
}
