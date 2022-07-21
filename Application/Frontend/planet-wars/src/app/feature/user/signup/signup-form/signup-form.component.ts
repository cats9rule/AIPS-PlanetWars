import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { UserCreateDto } from '../../dtos/userCreateDto';
import { User } from '../../interfaces/user';
import { userSignup } from '../../state/user.actions';
import { getUserLogged } from '../../state/user.selectors';
import { UserState } from '../../state/user.state';


@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.scss']
})
export class SignupFormComponent implements OnInit {

  public username: string = '';
  public password: string = '';
  public displayedName: string = '';
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

  public signUp() {
    if (this.username != '' && this.password != '' && this.displayedName != '') {
      const userCreateDto: UserCreateDto = {
        password: this.password,
        username: this.username,
        displayedName: this.displayedName
      };
      this.store.dispatch(userSignup({ userCreateDto }));
      console.log(userCreateDto);
    } else alert('Input username, password and nickname please!');
  }

}
