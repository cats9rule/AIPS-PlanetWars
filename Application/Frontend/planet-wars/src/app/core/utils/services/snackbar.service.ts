import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarMessageComponent } from '../../components/snackbar-message/snackbar-message.component';
import { SnackbarMessage } from '../../interfaces/snackbar-message';

@Injectable()
export class SnackbarService {
  constructor(private snackBar: MatSnackBar) {}

  private durationShort = 5000;
  private durationLong = 10000;

  public showMessage(message: SnackbarMessage, duration: 'short' | 'long') {
    this.snackBar.openFromComponent(SnackbarMessageComponent, {
      duration: duration == 'short' ? this.durationShort : this.durationLong,
      data: message,
      horizontalPosition: 'end',
      verticalPosition: 'bottom',
    });
  }
}
