import { GameMapDto } from './gameMapDto';
import { PlanetDto } from './planetDto';

export interface GalaxyDto {
  id: string;
  planets: PlanetDto[];
  gameMap: GameMapDto;
}
