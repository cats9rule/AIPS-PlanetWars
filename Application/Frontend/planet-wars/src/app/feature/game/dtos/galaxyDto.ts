import { GameMapDto } from './gameMapDto';

export interface GalaxyDto {
  id: string;
  planets: string[];
  gameMap: GameMapDto;
}
