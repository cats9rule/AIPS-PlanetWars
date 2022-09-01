import { createReducer, on } from '@ngrx/store';
import { setTurnActionDialogResult } from 'core/state/common.actions';
import { isDefined } from 'src/app/core/utils/types/maybe.type';
import { GalaxyDto } from '../../dtos/galaxyDto';
import { PlanetDto } from '../../dtos/planetDto';
import { SessionDto } from '../../dtos/sessionDto';
import {
  createGameSuccess,
  joinGameSuccess,
} from '../../lobby/state/lobby.actions';
import { Planet } from '../interfaces/planet';
import {
  addNewPlayer,
  constructGalaxySuccess,
  constructPlanetConnectionsRenderInfoSuccess,
  constructPlanetRenderInfoSuccess,
  placingArmies,
  resetAll,
  setRenderWindow,
  setSessionState,
  updatePlanet,
  updatePlanetOwner,
  updateSession,
} from './session.actions';
import { initialSessionState, SessionState } from './session.state';
import { cloneDeep } from 'lodash';
import { TurnActionDialogResult } from 'core/interfaces/turn-action-dialog-result';
import { ActionType } from 'core/enums/actionType.enum';

export const sessionReducer = createReducer(
  initialSessionState,
  on(
    createGameSuccess,
    joinGameSuccess,
    (state: SessionState, { sessionDto, userID }) => {
      return setSession(state, sessionDto, userID);
    }
  ),
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
  on(updatePlanetOwner, (state: SessionState, { planet, newOwnerID }) => {
    return updatePlanetOwnership(state, planet, newOwnerID);
  }),
  on(updatePlanet, (state: SessionState, { planet }) => {
    return updatePlanetInState(state, planet);
  }),
  on(setTurnActionDialogResult, (state: SessionState, { result }) => {
    if (result.armyCount > 0) {
      return executeAction(state, result);
    } else return state;
  }),
  on(setSessionState, (state: SessionState, { sessionState }) => {
    return sessionState;
  }),
  on(updateSession, (state: SessionState, { gameUpdate }) => {
    let armies: number = 0;
    const onTurn =
      gameUpdate.session.currentTurnIndex == state.player!!.turnIndex;
    if (onTurn) {
      armies = gameUpdate.armiesNextTurn;
    }
    return {
      ...state,
      session: gameUpdate.session,
      isOnTurn: onTurn,
      armiesToPlace: armies,
    };
  }),
  on(setRenderWindow, (state: SessionState, { width, height }) => {
    return {
      ...state,
      renderHeight: height,
      renderWidth: width,
    };
  }),
  on(resetAll, (state: SessionState) => {
    return initialSessionState;
  })
);

/**
 * HELPER METHODS
 */

const setSession = (
  state: SessionState,
  sessionDto: SessionDto,
  userID: string
) => {
  let planets = sessionDto.galaxy.planets.slice();
  // const gal: GalaxyDto = ;
  // const session: SessionDto = ;
  const player = sessionDto.players.find((p) => p.userID == userID);
  return {
    ...state,
    session: {
      ...sessionDto,
      galaxy: {
        ...sessionDto.galaxy,
        planets: planets,
      },
    },
    player: player,
    isOnTurn: player!!.turnIndex == sessionDto.currentTurnIndex,
  };
};

const updatePlanetOwnership = (
  state: SessionState,
  planet: PlanetDto,
  newOwnerID: string
): SessionState => {
  const planetIndex = state.planets.findIndex((p) => p.getID() == planet.id);
  if (planetIndex != -1) {
    const newState = { ...state };
    newState.planets[planetIndex].setOwnerID(newOwnerID);
    return newState;
  }
  return state;
};

const updatePlanetInState = (state: SessionState, planet: Planet) => {
  const planetIndex = state.planets.findIndex(
    (p) => p.getID() == planet.getID()
  );
  if (planetIndex != -1) {
    const planets = state.planets.slice();
    planets[planetIndex] = planet;
    return {
      ...state,
      planets: planets,
    };
  }
  return state;
};

const executeAction = (state: SessionState, result: TurnActionDialogResult) => {
  console.log(result);
  switch (result.actionType) {
    case ActionType.Placement: {
      return executePlacement(state, result);
    }
    case ActionType.Movement: {
      return executeMovement(state, result);
    }
    case ActionType.Attack: {
      return executeAttack(state, result);
    }
    default:
      return state;
  }
};

const executePlacement = (
  state: SessionState,
  result: TurnActionDialogResult
) => {
  if (state.armiesToPlace >= result.armyCount) {
    const planets = state.planets.slice();
    const planet = cloneDeep(
      planets.find(
        (p) => p.getID() == result.planetIDs[result.planetIDs.length - 1]
      )!!
    );
    planet!!.incrementArmyCount(result.armyCount);
    planets[planet!!.getIndexInGalaxy()] = planet;
    return {
      ...state,
      planets: planets,
      armiesToPlace: state.armiesToPlace - result.armyCount,
    };
  }
  return state;
};

const executeMovement = (
  state: SessionState,
  result: TurnActionDialogResult
) => {
  const planets = state.planets.slice();
  const planetFrom = cloneDeep(
    planets.find((p) => p.getID() == result.planetIDs[0])
  );
  if (planetFrom != undefined) {
    const planetTo = cloneDeep(
      planets.find((p) => p.getID() == result.planetIDs[1])
    );
    console.log(planetTo);
    if (planetTo != undefined) {
      planetFrom.incrementArmyCount(-result.armyCount);
      planetTo.incrementArmyCount(result.armyCount);
      planets[planetFrom.getIndexInGalaxy()] = planetFrom;
      planets[planetTo.getIndexInGalaxy()] = planetTo;
      return {
        ...state,
        planets: planets,
      };
    }
  }
  return state;
};

const executeAttack = (state: SessionState, result: TurnActionDialogResult) => {
  console.log('Executing attack');
  const planets = state.planets.slice();
  const planetFrom = cloneDeep(
    planets.find((p) => p.getID() == result.planetIDs[0])
  );
  if (
    planetFrom != undefined &&
    planetFrom!!.getArmyCount() >= result.armyCount
  ) {
    const planetTo = cloneDeep(
      planets.find((p) => p.getID() == result.planetIDs[1])
    );
    if (planetTo != undefined) {
      planetFrom.incrementArmyCount(-result.armyCount);
      planetTo.setArmyCount(
        planetTo.getArmyCount() * planetTo.getDefense() -
          result.armyCount * planetFrom.getAttack()
      );
      planets[planetFrom.getIndexInGalaxy()] = planetFrom;
      planets[planetTo.getIndexInGalaxy()] = planetTo;
      return {
        ...state,
        planets: planets,
      };
    }
  }
  return state;
};
