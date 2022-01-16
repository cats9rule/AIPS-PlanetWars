import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';
import { User } from 'src/interfaces/user';
import { UserDto } from 'src/interfaces/user-dto';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

  public loginUser(username: string, password: string): Observable<UserDto> {
    const uname = username.slice(0, username.length - 5);
    const tag = username.slice(username.length - 4);
    const userDto = {
      username: uname,
      tag,
      password
    };
    return this.http.post<UserDto>(environment.serverURL + "User/LogInUser", userDto);
  }
}
