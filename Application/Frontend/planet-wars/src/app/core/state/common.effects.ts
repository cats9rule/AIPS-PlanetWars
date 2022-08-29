import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { act, Actions, createEffect, ofType } from '@ngrx/effects';
import { ConfirmationDialogComponent } from 'core/components/confirmation-dialog/confirmation-dialog.component';
import { InfoDialogComponent } from 'core/components/info-dialog/info-dialog.component';
import { TurnActionDialogComponent } from 'core/components/turn-action-dialog/turn-action-dialog.component';
import { values } from 'lodash';
import { mergeMap, tap } from 'rxjs';
import { setTurnActionDialogResult } from './common.actions';
import { openActionDialog, openInfoDialog } from './dialog.actions';

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

  openInfoDialog$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(openInfoDialog),
        tap((action) => {
          this.dialog.open(InfoDialogComponent, { data: action.data });
        })
      ),
    { dispatch: false }
  );
}
