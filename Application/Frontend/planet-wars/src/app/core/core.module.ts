import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnackbarMessageComponent } from './components/snackbar-message/snackbar-message.component';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { SnackbarService } from './utils/services/snackbar.service';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { TurnActionDialogComponent } from './components/turn-action-dialog/turn-action-dialog.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [SnackbarMessageComponent, TurnActionDialogComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatSnackBarModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
  ],
  providers: [SnackbarService],
})
export class CoreModule {}
