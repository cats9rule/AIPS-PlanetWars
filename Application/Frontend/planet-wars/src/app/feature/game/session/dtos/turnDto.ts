import { ActionDto } from './actionDto';

export interface TurnDto {
  actions: ActionDto[];
  sessionID: string;
  playerID: string;
}
