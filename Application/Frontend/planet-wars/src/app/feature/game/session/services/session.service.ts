import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { PlayerDto } from '../../dtos/playerDto';
import { addNewPlayer } from '../state/session.actions';
import { SessionState } from '../state/session.state';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  constructor(private sessionStore: Store<SessionState>) {}

  public addPlayer(playerDto: PlayerDto) {
    this.sessionStore.dispatch(addNewPlayer({ playerDto }));
  }
}
