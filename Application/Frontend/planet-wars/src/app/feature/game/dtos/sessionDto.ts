import { GalaxyDto } from './galaxyDto';
import { PlayerDto } from './playerDto';

export interface SessionDto {
  id: string;
  name: string;
  gameCode: string;
  currentTurnIndex: number;
  players: PlayerDto[];
  playerCount: number;
  galaxy: GalaxyDto;
  maxPlayers: number;
}
