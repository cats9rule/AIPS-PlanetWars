export interface GameMapDto {
  id: string;
  planetCount: number;
  planetGraph: Map<number, number[]>;
  planetMatrix: boolean[];
  rows: number;
  columns: number;
  description: string;
  resourcePlanetRatio: number;
  maxPlayers: number;
}
