import { ActionType } from 'core/enums/actionType.enum';

export interface TurnActionDialogResult {
  actionType: ActionType;
  planetIDs: string[];
  armyCount: number;
}
