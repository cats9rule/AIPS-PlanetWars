import { Component, OnInit } from '@angular/core';
import { MatRow } from '@angular/material/table';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { GameMap } from '../../../interfaces/gameMap';
import { loadGameMaps } from '../../state/lobby.actions';
import { getGameMaps } from '../../state/lobby.selectors';
import { LobbyState } from '../../state/lobby.state';

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

  public gameMaps$: Observable<GameMap[]>;
  private gameMapsSubscription: Subscription = new Subscription();

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
    this.snackbarService.showMessage(
      { contents: `Clicked on row with ${row.id}.`, type: 'Info' },
      'short'
    );
    this.selectedMapID = row.id;
  }

  isRowSelected(row: any) {
    return this.selectedMapID == row.id;
  }

  createGame() {}
}
