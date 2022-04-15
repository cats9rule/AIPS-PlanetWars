import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnackbarMessageComponent } from './components/snackbar-message/snackbar-message.component';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { SnackbarService } from './utils/services/snackbar.service';

@NgModule({
  declarations: [SnackbarMessageComponent],
  imports: [CommonModule, MatCardModule, MatSnackBarModule],
  providers: [SnackbarService],
})
export class CoreModule {}
