import { createAction, props } from '@ngrx/store';
import { InfoDialogData } from 'core/interfaces/info-dialog-data';
import { TurnActionDialogData } from 'core/interfaces/turn-action-dialog-data';

export const openActionDialog = createAction(
  '[Action Dialog] Open Dialog',
  props<{ data: TurnActionDialogData }>()
);

export const openInfoDialog = createAction(
  '[Info Dialog] Open Dialog',
  props<{ data: InfoDialogData }>()
);

// export const openConfirmationDialog = createAction(
//   '[COnfirm Dialog] Open Dialog'
// );
