import { Component } from '@angular/core';
import { UserDto } from 'src/interfaces/user-dto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'messaging-demo';
  user: UserDto | undefined;
  isLoggedIn: boolean = false;
  constructor() { }

  public logInUser(user: UserDto | undefined): boolean {
    this.user = user;
    this.isLoggedIn = true;
    return true;
  }


}
