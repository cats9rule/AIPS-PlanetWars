import { GameMapDto } from '../../dtos/gameMapDto';

export interface LobbyState {
  gameMaps: GameMapDto[];
}

export const initialLobbyState: LobbyState = {
  gameMaps: [],
};
