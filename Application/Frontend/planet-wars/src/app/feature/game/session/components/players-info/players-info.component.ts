import { Component, Input, OnInit } from '@angular/core';
import { PlayerDto } from '../../../dtos/playerDto';

@Component({
  selector: 'app-players-info',
  templateUrl: './players-info.component.html',
  styleUrls: ['./players-info.component.scss'],
})
export class PlayersInfoComponent implements OnInit {
  @Input()
  public playersList: PlayerDto[] | undefined = [];
  @Input()
  public currentTurnIndex = 0;

  constructor() {}

  ngOnInit(): void {}

  isPlayersTurn(playerTurnIndex: number | undefined) {
    if (playerTurnIndex != undefined) {
      return playerTurnIndex == this.currentTurnIndex;
    }
    return false;
  }
}
