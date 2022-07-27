export interface Session {
  ID: string;
  name: string;
  password: string;
  currentTurnIndex: number;
  playerIDs: string[];
  maxPlayers: number;
  playerCount: number;
}
