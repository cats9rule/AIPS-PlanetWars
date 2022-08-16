import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { Maybe } from './core/utils/types/maybe.type';
import { getIsInSession } from './feature/game/session/state/session.selectors';
import { SessionState } from './feature/game/session/state/session.state';
import { User } from './feature/user/interfaces/user';
import {
  getIsUserLogged,
  getUserLogged,
} from './feature/user/state/user.selectors';
import { UserState } from './feature/user/state/user.state';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Planet Wars';

  private isUserLogged$: Observable<boolean>;
  public isUserLogged: boolean = false;
  private userSubscription: Subscription = new Subscription();

  private isInGame$: Observable<boolean>;
  public isInGame: boolean = false;
  private isInGameSubscription: Subscription = new Subscription();

  public user$: Observable<Maybe<User>>;

  public isLightTheme = false;

  constructor(
    private userStore: Store<UserState>,
    private sessionStore: Store<SessionState>
  ) {
    this.isUserLogged$ = this.userStore.select<boolean>(getIsUserLogged);
    this.isInGame$ = this.sessionStore.select<boolean>(getIsInSession);
    this.user$ = this.userStore.select<Maybe<User>>(getUserLogged);
  }

  ngOnInit(): void {
    this.userSubscription = this.isUserLogged$.subscribe({
      next: (isLogged) => {
        this.isUserLogged = isLogged;
      },
      error: (err) => {
        console.error(err);
      },
    });
    this.isInGameSubscription = this.isInGame$.subscribe({
      next: (isInGame) => {
        this.isInGame = isInGame;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
    this.isInGameSubscription.unsubscribe();
  }
}
