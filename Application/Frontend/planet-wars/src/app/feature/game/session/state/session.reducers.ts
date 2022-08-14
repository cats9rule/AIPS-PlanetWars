import { createReducer, on } from '@ngrx/store';
import { SessionDto } from '../../dtos/sessionDto';
import {
  createGameSuccess,
  joinGameSuccess,
} from '../../lobby/state/lobby.actions';
import {
  constructGalaxySuccess,
  constructPlanetConnectionsRenderInfoSuccess,
  constructPlanetRenderInfoSuccess,
} from './session.actions';
import { initialSessionState, SessionState } from './session.state';

export const sessionReducer = createReducer(
  initialSessionState,
  on(createGameSuccess, (state: SessionState, { sessionDto }) => {
    return setSession(state, sessionDto);
  }),
  on(joinGameSuccess, (state: SessionState, { sessionDto }) => {
    return setSession(state, sessionDto);
  }),
  on(constructGalaxySuccess, (state: SessionState, { planets }) => {
    return {
      ...state,
      planets: planets,
    };
  }),
  on(
    constructPlanetRenderInfoSuccess,
    (state: SessionState, { planetsRenderInfo }) => {
      return {
        ...state,
        planetsRenderInfo: planetsRenderInfo,
      };
    }
  ),
  on(
    constructPlanetConnectionsRenderInfoSuccess,
    (state: SessionState, { connectionsRenderInfo }) => {
      return {
        ...state,
        planetConnectionsInfo: connectionsRenderInfo,
      };
    }
  )
);

const setSession = (state: SessionState, sessionDto: SessionDto) => {
  return {
    ...state,
    session: sessionDto,
  };
};
