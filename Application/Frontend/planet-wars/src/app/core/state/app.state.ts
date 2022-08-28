import { LobbyState } from 'src/app/feature/game/lobby/state/lobby.state';
import { SessionState } from 'src/app/feature/game/session/state/session.state';
import { UserState } from 'src/app/feature/user/state/user.state';

export interface AppState {
  user: UserState;
  lobby: LobbyState;
  session: SessionState;
}
