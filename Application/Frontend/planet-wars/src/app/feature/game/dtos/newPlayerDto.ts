import { CommunicationParam } from '../interfaces/communicationParam';
import { PlayerDto } from './playerDto';

export interface NewPlayerDto extends CommunicationParam {
  sessionID: string;
  newPlayer: PlayerDto;
}
