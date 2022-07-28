import { Planet } from './planet';

export interface Galaxy {
  id: string;
  planets: Planet[];
  planetCount: number;
  resourcePlanetRatio: number;
  gameMapID: string;
}
