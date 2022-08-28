import { ActionType } from 'core/enums/actionType.enum';

export interface TurnActionDialogData {
  action: ActionType;
  availableArmies: number;
  planetID: string;
}
