import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { PlayerDto } from '../../dtos/playerDto';
import { SessionDto } from '../../dtos/sessionDto';
import { Planet } from '../interfaces/planet';
import { PlanetConnectionInfo } from '../interfaces/planetConnectionInfo';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';
import { SessionInfo } from '../interfaces/sessionInfo';

export interface SessionState {
  session: Maybe<SessionDto>;
  player: Maybe<PlayerDto>;
  planets: Planet[];
  planetsRenderInfo: PlanetRenderInfo[];
  planetConnectionsInfo: PlanetConnectionInfo[];
  sessionInfo: SessionInfo;
}

export const initialSessionState: SessionState = {
  session: null,
  player: null,
  planets: [],
  planetsRenderInfo: [],
  planetConnectionsInfo: [],
  sessionInfo: {
    attackingPlanet: false,
    movingArmies: false,
    placedArmies: false,
    placingArmies: false,
  },
};
