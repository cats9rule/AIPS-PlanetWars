import { PlanetDto } from "./planetDto";

export interface PlayerDto {
  id: string;
  userID: string;
  username: string;
  playerColor: string; //hex value
  turnIndex: number;
  planets: PlanetDto[];
  isActive: boolean;
  sessionID: string;
}
