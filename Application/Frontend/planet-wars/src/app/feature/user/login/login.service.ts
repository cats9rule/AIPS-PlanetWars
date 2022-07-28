import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';
import { SnackbarMessage } from 'src/app/core/interfaces/snackbar-message';
import { ServerErrorHandlerService } from 'src/app/core/utils/services/server-error-handler.service';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { environment } from 'src/environments/environment.prod';
import { UserDto } from '../dtos/userDto';
import { UserLoginDto } from '../dtos/userLoginDto';
import { User } from '../interfaces/user';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(
    private http: HttpClient,
    private errorHandler: ServerErrorHandlerService
  ) {}

  private url = environment.serverUrl + '/User/LogInUser';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  loginUser(user: UserLoginDto): Observable<UserDto> {
    return this.http
      .post<UserDto>(this.url, user)
      .pipe(catchError(this.errorHandler.handleError));
  }
}
