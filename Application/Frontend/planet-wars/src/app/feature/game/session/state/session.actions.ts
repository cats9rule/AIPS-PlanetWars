import { createAction, props } from '@ngrx/store';
import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { User } from 'src/app/feature/user/interfaces/user';
import { GalaxyDto } from '../../dtos/galaxyDto';
import { GameMapDto } from '../../dtos/gameMapDto';
import { GameUpdateDto } from '../../dtos/gameUpdateDto';
import { LeaveGameDto } from '../../dtos/leaveGameDto';
import { PlanetDto } from '../../dtos/planetDto';
import { PlayerDto } from '../../dtos/playerDto';
import { SessionDto } from '../../dtos/sessionDto';
import { TurnDto } from '../dtos/turnDto';
import { Planet } from '../interfaces/planet';
import { PlanetConnectionInfo } from '../interfaces/planetConnectionInfo';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';
import { SessionState } from './session.state';

export const joinSessionGroup = createAction(
  '[Session] Join Session Group',
  props<{ user: Maybe<User>; sessionID: string }>()
);

export const constructGalaxy = createAction(
  '[Galaxy] Construct Galaxy',
  props<{ galaxyDto: GalaxyDto; matrixWidth: number; matrixHeight: number }>()
);

export const constructGalaxySuccess = createAction(
  '[Galaxy] Construct Galaxy Success',
  props<{ planets: Planet[] }>()
);

export const constructPlanetRenderInfo = createAction(
  '[Galaxy] Construct Planet Render Info'
);

export const constructPlanetRenderInfoSuccess = createAction(
  '[Galaxy] Construct Planet Render Info Success',
  props<{ planetsRenderInfo: PlanetRenderInfo[] }>()
);

export const constructPlanetConnectionsRenderInfo = createAction(
  '[Galaxy] Construct Planet Connections Render Info'
);

export const constructPlanetConnectionsRenderInfoSuccess = createAction(
  '[Galaxy] Construct Planet Connections Render Info Success',
  props<{ connectionsRenderInfo: PlanetConnectionInfo[] }>()
);

export const addNewPlayer = createAction(
  '[Session] Add New Player',
  props<{ playerDto: PlayerDto }>()
);

export const updatePlanetOwner = createAction(
  '[Galaxy] Update Planet Owner',
  props<{ planet: PlanetDto; newOwnerID: string }>()
);

export const updatePlanet = createAction(
  '[Galaxy] Update Planet',
  props<{ planet: Planet }>()
);

export const placingArmies = createAction(
  '[Actions] Placing Armies',
  props<{ placingArmies: boolean }>()
);

export const setSessionState = createAction(
  '[Session] Set Session State',
  props<{ sessionState: SessionState }>()
);

export const updateSession = createAction(
  '[Session] Update Session',
  props<{ gameUpdate: GameUpdateDto }>()
);

export const playMove = createAction(
  '[Session] Play Move',
  props<{ turnDto: TurnDto }>()
);

export const setRenderWindow = createAction(
  '[Galaxy] Set Render Window',
  props<{ width: number; height: number }>()
);

export const resetAll = createAction('[Session] Reset Everything');

export const leaveGame = createAction(
  '[Session] Leave Game',
  props<{ leaveGameDto: LeaveGameDto }>()
);

export const leaveGameSuccess = createAction('[Session] Leave Game Success');
