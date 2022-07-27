import { Session } from 'inspector';
import { Galaxy } from '../interfaces/galaxy';

export interface GameState {
  session: Session;
  galaxy: Galaxy;
}
