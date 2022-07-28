import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatRow } from '@angular/material/table';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { GameMap } from '../../../interfaces/gameMap';
import { createGame, loadGameMaps } from '../../state/lobby.actions';
import { getGameMaps } from '../../state/lobby.selectors';
import { LobbyState } from '../../state/lobby.state';
import { CreateGameDto } from '../../dtos/createGameDto';
import { UserState } from 'src/app/feature/user/state/user.state';
import { getUserID } from 'src/app/feature/user/state/user.selectors';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';

@Component({
  selector: 'app-create-game-form',
  templateUrl: './create-game-form.component.html',
  styleUrls: ['./create-game-form.component.scss'],
})
export class CreateGameFormComponent implements OnInit, OnDestroy {
  public panelOpenState = false;
  public sessionName = '';
  public displayedColumns: string[] = [
    'planetCount',
    'resourcePlanetRatio',
    'maxPlayers',
    'description',
  ];

  public gameMaps$: Observable<GameMap[]>;

  private selectedMapID: string = '';

  private userID$: Observable<Maybe<string>>;

  private userIDSubscription: Subscription = new Subscription();

  private userID = '';

  constructor(
    private store: Store<LobbyState>,
    private userStore: Store<UserState>,
    private snackbarService: SnackbarService
  ) {
    this.gameMaps$ = this.store.select<GameMap[]>(getGameMaps);
    this.userID$ = this.userStore.select<string>(getUserID);
    this.store.dispatch(loadGameMaps());
  }

  ngOnInit(): void {
    this.userIDSubscription = this.userID$.subscribe({
      next: (userID) => {
        if (isDefined(userID)) {
          this.userID = userID!!;
        }
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

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

  ngOnDestroy(): void {
    this.userIDSubscription.unsubscribe();
  }
}
