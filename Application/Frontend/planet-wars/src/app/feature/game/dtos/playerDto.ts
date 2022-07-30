export interface PlayerDto {
  id: string;
  userID: string;
  username: string;
  playerColor: string; //hex value
  turnIndex: number;
  planetIDs: string[];
  isActive: boolean;
  sessionID: string;
}
