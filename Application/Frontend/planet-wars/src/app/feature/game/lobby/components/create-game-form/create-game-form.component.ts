import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { GameMap } from '../../../interfaces/gameMap';
import { createGame, loadGameMaps } from '../../state/lobby.actions';
import { getGameMaps } from '../../state/lobby.selectors';
import { LobbyState } from '../../state/lobby.state';
import { CreateGameDto } from '../../dtos/createGameDto';

@Component({
  selector: 'app-create-game-form',
  templateUrl: './create-game-form.component.html',
  styleUrls: ['./create-game-form.component.scss'],
})
export class CreateGameFormComponent implements OnInit {
  public panelOpenState = false;
  public sessionName = '';
  public displayedColumns: string[] = [
    'planetCount',
    'resourcePlanetRatio',
    'maxPlayers',
    'description',
  ];

  @Output()
  public onBackEvent = new EventEmitter<string>();

  @Input()
  public userID = '';

  public gameMaps$: Observable<GameMap[]>;

  private selectedMapID: string = '';

  constructor(
    private store: Store<LobbyState>,
    private snackbarService: SnackbarService
  ) {
    this.gameMaps$ = this.store.select<GameMap[]>(getGameMaps);
    this.store.dispatch(loadGameMaps());
  }

  ngOnInit(): void {}

  onSelectRow(row: any) {
    this.selectedMapID = row.id;
    this.snackbarService.showMessage(
      { contents: `Clicked on row with ${this.selectedMapID}.`, type: 'Info' },
      'short'
    );
  }

  isRowSelected(row: any) {
    return this.selectedMapID == row.id;
  }

  createGame() {
    const createGameDto: CreateGameDto = {
      gameMapID: this.selectedMapID,
      userID: this.userID,
      name: this.sessionName,
    };
    this.store.dispatch(createGame({ createGameDto }));
  }

  onBack() {
    this.onBackEvent.emit('back');
  }
}
