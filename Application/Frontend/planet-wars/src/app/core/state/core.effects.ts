import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TurnActionDialogComponent } from 'core/components/turn-action-dialog/turn-action-dialog.component';
import { mergeMap } from 'rxjs';
import { openActionDialog } from './dialog.actions';

@Injectable()
export class CoreEffects {
  constructor(private actions$: Actions, private dialog: MatDialog) {}

  // openActionDialog$ = createEffect(() =>
  // this.actions$.pipe(
  //     ofType(openActionDialog),
  //     mergeMap((action) => {
  //         const dialogRef = this.dialog.open(TurnActionDialogComponent, {data: action.data} );
  //         dialogRef.afterClosed().subscribe({
  //             next: (value: number) => {

  //             }
  //         })
  //     })
  // ))

  //TODO: smisli kako da u session main preuzmes dialog res
}
