import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Store } from '@ngrx/store';
import { JoinGameDto } from '../../dtos/joinGameDto';
import { joinGame } from '../../state/lobby.actions';
import { LobbyState } from '../../state/lobby.state';

@Component({
  selector: 'app-join-game-form',
  templateUrl: './join-game-form.component.html',
  styleUrls: ['./join-game-form.component.scss'],
})
export class JoinGameFormComponent implements OnInit {
  public sessionName = '';
  public gameCode = '';

  @Output()
  public onBackEvent = new EventEmitter<string>();

  @Input()
  public userID = '';

  constructor(private store: Store<LobbyState>) {}

  ngOnInit(): void {}

  joinGame() {
    const joinGameDto: JoinGameDto = {
      gameCode: this.gameCode,
      sessionName: this.sessionName,
      userID: this.userID,
    };
    this.store.dispatch(joinGame({ joinGameDto }));
  }

  onBack() {
    this.onBackEvent.emit('back');
  }
}
