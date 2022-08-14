import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { PlayerDto } from '../../dtos/playerDto';
import { SessionDto } from '../../dtos/sessionDto';
import { Planet } from '../interfaces/planet';
import { PlanetConnectionInfo } from '../interfaces/planetConnectionInfo';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';

export interface SessionState {
  session: Maybe<SessionDto>;
  player: Maybe<PlayerDto>;
  planets: Planet[];
  planetsRenderInfo: Map<number, PlanetRenderInfo[]>;
  planetConnectionsInfo: PlanetConnectionInfo[];
}

export const initialSessionState: SessionState = {
  session: null,
  player: null,
  planets: [],
  planetsRenderInfo: new Map(),
  planetConnectionsInfo: [],
};
