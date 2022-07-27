export interface CreateGameDto {
    userID: string,
    gameMapID: string,
    name: string,
    password: string,
    maxPlayers: number
}