import { CommunicationParam } from '../../interfaces/communicationParam';

export interface MessageDto extends CommunicationParam {
  sessionID: string;
  usernameWithTag: string;
  contents: string;
}
