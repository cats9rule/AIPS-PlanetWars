import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { getIsUserLogged } from './feature/user/state/user.selectors';
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

  constructor(private store: Store<UserState>) {
    this.isUserLogged$ = this.store.select<boolean>(getIsUserLogged);
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
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }
}
