export interface SessionDto {
  ID: string;
  name: string;
  gameCode: string;
  currentTurnIndex: number;
  playerIDs: string[];
  playerCount: number;
  galaxyID: string;
  maxPlayers: number;
}
