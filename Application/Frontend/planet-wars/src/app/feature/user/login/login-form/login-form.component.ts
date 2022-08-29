import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { UserLoginDto } from '../../dtos/userLoginDto';
import { User } from '../../interfaces/user';
import { userLogin } from '../../state/user.actions';
import { getUserLogged } from '../../state/user.selectors';
import { UserState } from '../../state/user.state';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit, OnDestroy {
  public username: string = '';
  public password: string = '';
  hide = true;
  private user$: Observable<Maybe<User>>;
  private user: Maybe<User>;
  private userSubscription: Subscription = new Subscription();

  constructor(private store: Store<UserState>) {
    this.user$ = this.store.select<Maybe<User>>(getUserLogged);
  }

  ngOnInit(): void {
    this.userSubscription = this.user$.subscribe({
      next: (user) => {
        if (isDefined(user)) {
          this.user = user;
        }
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  public logIn() {
    if (this.username != '' && this.password != '') {
      const usernameAndTagArray = this.username.split('#');
      const userLoginDto: UserLoginDto = {
        password: this.password,
        tag: usernameAndTagArray[1],
        username: usernameAndTagArray[0],
      };
      this.store.dispatch(userLogin({ userLoginDto }));
    } else alert('Input username and password please!');
  }
}
