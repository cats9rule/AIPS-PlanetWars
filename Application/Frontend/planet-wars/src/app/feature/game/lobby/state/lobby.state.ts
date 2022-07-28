import { GameMap } from '../../interfaces/gameMap';

export interface LobbyState {
  gameMaps: GameMap[];
}

export const initialLobbyState: LobbyState = {
  gameMaps: [],
};
