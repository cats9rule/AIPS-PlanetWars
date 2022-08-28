import { ActionType } from 'core/enums/actionType.enum';

export interface TurnActionDialogResult {
  actionType: ActionType;
  planetID: string;
  armyCount: number;
}
