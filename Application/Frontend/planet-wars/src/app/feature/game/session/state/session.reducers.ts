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
  updatePlanetOwner,
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
  ),
  on(updatePlanetOwner, (state: SessionState, { planetID, newOwnerID }) => {
    return updatePlanetOwnership(state, planetID, newOwnerID);
  })
);

const setSession = (state: SessionState, sessionDto: SessionDto) => {
  return {
    ...state,
    session: sessionDto,
  };
};

const updatePlanetOwnership = (
  state: SessionState,
  planetID: string,
  newOwnerID: string
): SessionState => {
  const planetIndex = state.planets.findIndex((p) => p.getID() == planetID);
  if (planetIndex != -1) {
    const newState = state;
    newState.planets[planetIndex].setOwnerID(newOwnerID);
    return newState;
  }
  return state;
};
