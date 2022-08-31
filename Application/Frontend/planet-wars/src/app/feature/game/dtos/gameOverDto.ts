import { CommunicationParam } from '../interfaces/communicationParam';
import { PlayerDto } from './playerDto';

export interface GameOverDto extends CommunicationParam {
  sessionID: string;
  winner: PlayerDto;
}
