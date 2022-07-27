export interface SessionDto {
    ID: string,
    name: string,
    password: string,
    currentTurnIndex: number,
    playerIDs: string[],
    playerCount: number,
    galaxyID: string,
    maxPlayers: number
}