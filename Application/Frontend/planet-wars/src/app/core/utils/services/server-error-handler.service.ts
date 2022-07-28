import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { SnackbarMessage } from '../../interfaces/snackbar-message';
import { SnackbarService } from './snackbar.service';

@Injectable({
  providedIn: 'root',
})
export class ServerErrorHandlerService {
  constructor(private snackbarService: SnackbarService) {}

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
