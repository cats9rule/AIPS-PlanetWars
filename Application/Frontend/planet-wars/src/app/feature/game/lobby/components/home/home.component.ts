import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { getUserID } from 'src/app/feature/user/state/user.selectors';
import { UserState } from 'src/app/feature/user/state/user.state';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit, OnDestroy {
  public isDisplayingMenu = true;
  public isDisplayingCreateGame = false;
  public isDisplayingJoinGame = false;

  private userID$: Observable<Maybe<string>>;
  private userIDSubscription: Subscription = new Subscription();
  public userID = '';

  constructor(private userStore: Store<UserState>) {
    this.userID$ = this.userStore.select<string>(getUserID);
  }

  ngOnInit(): void {
    this.userIDSubscription = this.userID$.subscribe({
      next: (userID) => {
        if (isDefined(userID)) {
          this.userID = userID!!;
        }
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  handleNavigation(clickedOn: string) {
    if (clickedOn == 'createGame') {
      this.isDisplayingMenu = false;
      this.isDisplayingCreateGame = true;
      this.isDisplayingJoinGame = false;
    }
    if (clickedOn == 'joinGame') {
      this.isDisplayingMenu = false;
      this.isDisplayingCreateGame = false;
      this.isDisplayingJoinGame = true;
    }
    if (clickedOn == 'back') {
      this.isDisplayingMenu = true;
      this.isDisplayingCreateGame = false;
      this.isDisplayingJoinGame = false;
    }
  }

  ngOnDestroy(): void {
    this.userIDSubscription.unsubscribe();
  }
}
