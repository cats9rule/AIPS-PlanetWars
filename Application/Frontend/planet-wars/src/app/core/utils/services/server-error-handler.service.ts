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
    console.log(error);
    if (error.status === 0) {
      this.log({
        type: 'Error',
        contents: `An error occurred: ${error.message}`,
      });
    } else {
      let details = '';
      if (error.error.detail != undefined) {
        details = ' ' + error.error.detail;
      }
      this.log({
        type: 'Error',
        contents: `Status ${error.status}:  ${error.error.title}${details}`,
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
