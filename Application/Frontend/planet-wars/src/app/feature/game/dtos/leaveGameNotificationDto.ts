import { GameUpdateDto } from './gameUpdateDto';
import { SessionDto } from './sessionDto';

export interface LeaveGameNotificationDto {
  gameUpdate: GameUpdateDto;
  playerID: string;
  message: string;
}
