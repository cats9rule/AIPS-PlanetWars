import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { act, Actions, createEffect, ofType } from '@ngrx/effects';
import { TurnActionDialogComponent } from 'core/components/turn-action-dialog/turn-action-dialog.component';
import { mergeMap } from 'rxjs';
import { setTurnActionDialogResult } from './common.actions';
import { openActionDialog } from './dialog.actions';

@Injectable()
export class CommonEffects {
  constructor(private actions$: Actions, private dialog: MatDialog) {}

  openActionDialog$ = createEffect(() =>
    this.actions$.pipe(
      ofType(openActionDialog),
      mergeMap((action) => {
        const dialogRef = this.dialog.open(TurnActionDialogComponent, {
          data: action.data,
        });
        return dialogRef.afterClosed().pipe(
          mergeMap((value) => {
            return [
              setTurnActionDialogResult({
                result: {
                  planetID: action.data.planetID,
                  actionType: action.data.action,
                  armyCount: value,
                },
              }),
            ];
          })
        );
      })
    )
  );
}
