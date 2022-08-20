import { createReducer, on } from '@ngrx/store';
import { isDefined } from 'src/app/core/utils/types/maybe.type';
import { GalaxyDto } from '../../dtos/galaxyDto';
import { SessionDto } from '../../dtos/sessionDto';
import {
  createGameSuccess,
  joinGameSuccess,
} from '../../lobby/state/lobby.actions';
import {
  addNewPlayer,
  constructGalaxySuccess,
  constructPlanetConnectionsRenderInfoSuccess,
  constructPlanetRenderInfoSuccess,
  updatePlanetOwner,
} from './session.actions';
import { initialSessionState, SessionState } from './session.state';

export const sessionReducer = createReducer(
  initialSessionState,
  on(createGameSuccess, (state: SessionState, { sessionDto, userID }) => {
    return setSession(state, sessionDto, userID);
  }),
  on(joinGameSuccess, (state: SessionState, { sessionDto, userID }) => {
    return setSession(state, sessionDto, userID);
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
  on(addNewPlayer, (state: SessionState, { playerDto }) => {
    if (isDefined(state.session)) {
      const session: SessionDto = {
        currentTurnIndex: state.session!!.currentTurnIndex,
        galaxy: state.session!!.galaxy,
        gameCode: state.session!!.gameCode,
        id: state.session!!.id,
        maxPlayers: state.session!!.maxPlayers,
        name: state.session!!.name,
        playerCount: state.session!!.playerCount + 1,
        players: [...state.session!!.players, playerDto],
      };

      return {
        ...state,
        session: session,
      };
    }
    return state;
  }),
  on(updatePlanetOwner, (state: SessionState, { planetID, newOwnerID }) => {
    return updatePlanetOwnership(state, planetID, newOwnerID);
  })
);

const setSession = (
  state: SessionState,
  sessionDto: SessionDto,
  userID: string
) => {
  let planets = sessionDto.galaxy.planets.slice();
  planets.sort((p1, p2) => p1.indexInGalaxy - p2.indexInGalaxy);
  const gal: GalaxyDto = {
    ...sessionDto.galaxy,
    planets: planets
  }
  const session: SessionDto = {
    ...sessionDto,
    galaxy: gal
  }
  return {
    ...state,
    session: session,
    player: sessionDto.players.find((p) => p.userID == userID),
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
