import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { getMatInputUnsupportedTypeError } from '@angular/material/input';
import { LoginService } from 'src/app/services/login.service';
import { environment } from 'src/environments/environment.prod';
import { User } from 'src/interfaces/user';
import { UserDto } from 'src/interfaces/user-dto';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  @Output() userLoginEvent = new EventEmitter<UserDto>();
  public username: string = '';
  public password: string = '';

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

  public logIn() {
    if (this.username != "" && this.password != "") {
      this.loginService.loginUser(this.username, this.password).subscribe({
        next: dto => {
          console.log(dto);
          this.userLoginEvent.emit(dto);
        }
      });
    }
    else alert("Input username and password please!");
  }

}
