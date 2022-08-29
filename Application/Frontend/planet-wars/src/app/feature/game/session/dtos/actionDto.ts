import { ActionType } from '../../../../core/enums/actionType.enum';

export interface ActionDto {
  playerID: string;
  planetFrom: string;
  planetTo: string;
  armies: number;
  type: ActionType;
}
