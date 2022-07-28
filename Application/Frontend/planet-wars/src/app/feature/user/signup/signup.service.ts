import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { SnackbarMessage } from 'src/app/core/interfaces/snackbar-message';
import { ServerErrorHandlerService } from 'src/app/core/utils/services/server-error-handler.service';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { environment } from 'src/environments/environment.prod';
import { UserCreateDto } from '../dtos/userCreateDto';
import { UserDto } from '../dtos/userDto';

@Injectable({
  providedIn: 'root',
})
export class SignupService {
  constructor(
    private http: HttpClient,
    private errorHandler: ServerErrorHandlerService
  ) {}

  private url = environment.serverUrl + '/User/CreateUser';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  signupUser(user: UserCreateDto): Observable<UserDto> {
    return this.http
      .post<UserDto>(this.url, user)
      .pipe(catchError(this.errorHandler.handleError));
  }
}
