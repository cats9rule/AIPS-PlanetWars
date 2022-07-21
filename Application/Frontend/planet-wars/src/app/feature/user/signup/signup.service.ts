import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { SnackbarMessage } from 'src/app/core/interfaces/snackbar-message';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { environment } from 'src/environments/environment.prod';
import { UserCreateDto } from '../dtos/userCreateDto';
import { UserDto } from '../dtos/userDto';

@Injectable({
  providedIn: 'root'
})
export class SignupService {

  constructor(    private http: HttpClient,
    private snackbarService: SnackbarService) { }

    private url = environment.serverUrl + '/User/CreateUser';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  signupUser(user: UserCreateDto): Observable<UserDto> {
    return this.http
      .post<UserDto>(this.url, user)
      .pipe(catchError(this.handleError));
  }

  handleError = (error: HttpErrorResponse) => {
    if (error.status === 0) {
      this.log({
        type: 'Error',
        contents: `An error occurred: ${error.message}}`,
      });
    } else {
      this.log({
        type: 'Error',
        contents: `Backend returned code ${error.status}:  ${error.message}}`,
      });
    }
    return throwError(
      () => new Error('Something bad happened; please try again later.')
    );
  };

  log = (message: SnackbarMessage) => {
    this.snackbarService.showMessage(message, 'long');
  };
}
