import { createAction, props } from '@ngrx/store';
import { TurnActionDialogData } from 'core/interfaces/turn-action-dialog-data';

export const openActionDialog = createAction(
  '[Action Dialog] Open Dialog',
  props<{ data: TurnActionDialogData }>()
);
