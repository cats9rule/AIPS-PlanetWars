import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { ServerErrorHandlerService } from 'core/utils/services/server-error-handler.service';
import { catchError } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { GameUpdateDto } from '../../dtos/gameUpdateDto';
import { PlayerDto } from '../../dtos/playerDto';
import { TurnDto } from '../dtos/turnDto';
import { addNewPlayer, updateSession } from '../state/session.actions';
import { SessionState } from '../state/session.state';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  constructor(
    private sessionStore: Store<SessionState>,
    private http: HttpClient,
    private errorHandler: ServerErrorHandlerService
  ) {}

  private url = environment.serverUrl + '/Session/PlayMove';

  public addPlayer(playerDto: PlayerDto) {
    this.sessionStore.dispatch(addNewPlayer({ playerDto }));
  }

  public updateSession(gud: GameUpdateDto) {
    this.sessionStore.dispatch(updateSession({ gameUpdate: gud }));
  }

  public playMove(turnDto: TurnDto) {
    console.log(turnDto);
    return this.http
      .put<any>(this.url, turnDto)
      .pipe(catchError(this.errorHandler.handleError))
      .subscribe({
        next: (response) => {
          console.log(response);
        },
      });
  }
}
