import { createAction, props } from '@ngrx/store';
import { TurnActionDialogResult } from 'core/interfaces/turn-action-dialog-result';

export const noAction = createAction('[Common] No Action');

export const setTurnActionDialogResult = createAction(
  '[Common] Set Turn Action Dialog Result',
  props<{ result: TurnActionDialogResult }>()
);
