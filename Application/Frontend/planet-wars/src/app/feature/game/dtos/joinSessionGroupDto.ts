import { CommunicationParam } from '../interfaces/communicationParam';

export interface JoinSessionGroupDto extends CommunicationParam {
  sessionID: string;
  usernameWithTag: string;
}
