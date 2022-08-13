export interface GameMapDto {
  id: string;
  planetCount: number;
  planetGraph: any;
  planetMatrix: boolean[];
  rows: number;
  columns: number;
  description: string;
  resourcePlanetRatio: number;
  maxPlayers: number;
}
