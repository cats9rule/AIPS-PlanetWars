import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { PlayerDto } from '../../dtos/playerDto';
import { SessionDto } from '../../dtos/sessionDto';
import { Planet } from '../interfaces/planet';
import { PlanetConnectionInfo } from '../interfaces/planetConnectionInfo';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';
import { TurnInfo } from '../interfaces/turnInfo';

export interface SessionState {
  session: Maybe<SessionDto>;
  player: Maybe<PlayerDto>;
  planets: Planet[];
  planetsRenderInfo: PlanetRenderInfo[];
  planetConnectionsInfo: PlanetConnectionInfo[];
  armiesToPlace: number;
  isOnTurn: boolean;
  renderWidth: number;
  renderHeight: number;
}

export const initialSessionState: SessionState = {
  session: null,
  player: null,
  planets: [],
  planetsRenderInfo: [],
  planetConnectionsInfo: [],
  armiesToPlace: 5,
  isOnTurn: false,
  renderWidth: 0,
  renderHeight: 0,
};
