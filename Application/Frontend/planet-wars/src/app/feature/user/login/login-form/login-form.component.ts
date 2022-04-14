import { Component, OnInit } from '@angular/core';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit {
  public username: string = '';
  public password: string = '';
  hide = true;

  constructor(private loginService: LoginService) {}

  ngOnInit(): void {}

  public logIn() {
    if (this.username != '' && this.password != '') {
      alert(
        'Username and password set:' + this.username + ', ' + this.password
      );
    } else alert('Input username and password please!');
  }
}
