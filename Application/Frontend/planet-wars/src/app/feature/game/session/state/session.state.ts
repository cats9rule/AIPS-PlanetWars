import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { Galaxy } from '../../interfaces/galaxy';
import { GameMap } from '../../interfaces/gameMap';
import { Player } from '../../interfaces/player';
import { Session } from '../../interfaces/session';

export interface SessionState {
  session: Maybe<Session>;
  galaxy: Maybe<Galaxy>;
  gameMap: Maybe<GameMap>;
  players: Player[];
}

export const initialState: SessionState = {
  galaxy: null,
  session: null,
  gameMap: null,
  players: [],
};
