export interface Player {
  id: string;
  userID: string;
  username: string;
  playerColor: string; //hex value
  turnIndex: number;
  ownedPlanetIDs: string[];
  isActive: boolean;
  sessionID: string;
}
