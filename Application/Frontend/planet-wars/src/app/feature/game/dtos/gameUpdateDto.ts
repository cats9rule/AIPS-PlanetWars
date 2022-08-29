import { CommunicationParam } from '../interfaces/communicationParam';
import { SessionDto } from './sessionDto';

export interface GameUpdateDto extends CommunicationParam {
  session: SessionDto;
  armiesNextTurn: number;
}
