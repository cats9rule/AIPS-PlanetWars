import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { UserDto } from '../dtos/userDto';
import { UserLoginDto } from '../dtos/userLoginDto';
import { User } from '../interfaces/user';

@Injectable()
export class LoginService {
  constructor(private http: HttpClient) {}

  private url = environment.serverUrl + '/UserController/LogInUser';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  loginUser(user: UserLoginDto): Observable<User> {
    return this.http.post<UserDto>(this.url, user, this.httpOptions);
    //TODO: handle errors
  }
}
