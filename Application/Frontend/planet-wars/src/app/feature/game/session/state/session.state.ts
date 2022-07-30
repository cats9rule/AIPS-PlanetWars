import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { PlayerDto } from '../../dtos/playerDto';
import { SessionDto } from '../../dtos/sessionDto';

export interface SessionState {
  session: Maybe<SessionDto>;
  player: Maybe<PlayerDto>;
}

export const initialSessionState: SessionState = {
  session: null,
  player: null,
};
