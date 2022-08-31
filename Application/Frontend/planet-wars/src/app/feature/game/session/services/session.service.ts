import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { ActionType } from 'core/enums/actionType.enum';
import { openInfoDialog } from 'core/state/dialog.actions';
import { ServerErrorHandlerService } from 'core/utils/services/server-error-handler.service';
import { BehaviorSubject, catchError } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { GameUpdateDto } from '../../dtos/gameUpdateDto';
import { LeaveGameDto } from '../../dtos/leaveGameDto';
import { LeaveGameNotificationDto } from '../../dtos/leaveGameNotificationDto';
import { PlayerDto } from '../../dtos/playerDto';
import { TurnDto } from '../dtos/turnDto';
import {
  addNewPlayer,
  leaveGameSuccess,
  resetAll,
  updateSession,
} from '../state/session.actions';
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

  private url = environment.serverUrl + '/Session/';
  messageSource: BehaviorSubject<number> = new BehaviorSubject(-1);

  public addPlayer(playerDto: PlayerDto) {
    this.sessionStore.dispatch(addNewPlayer({ playerDto }));
  }

  public updateSession(gud: GameUpdateDto) {
    this.sessionStore.dispatch(updateSession({ gameUpdate: gud }));
  }

  public playMove(turnDto: TurnDto) {
    console.log(turnDto);
    this.messageSource.next(3);
    return this.http
      .put<any>(this.url + 'PlayMove', turnDto)
      .pipe(catchError(this.errorHandler.handleError))
      .subscribe({
        next: (response) => {
          console.log(response);
        },
      });
  }

  public notifyWinner(playerDto: PlayerDto) {
    this.sessionStore.dispatch(
      openInfoDialog({
        data: {
          title: 'WINNER',
          message: `The winner of this session is ${playerDto.username}. Congratulations!`,
        },
      })
    );
    this.sessionStore.dispatch(resetAll());
  }

  public leaveGame(ldg: LeaveGameDto) {
    this.http
      .put<any>(this.url + 'LeaveGame', ldg)
      .pipe(catchError(this.errorHandler.handleError))
      .subscribe({
        next: (response) => {
          console.log(response);
          this.sessionStore.dispatch(leaveGameSuccess());
        },
      });
  }

  public onPlayerLeftGame(lgn: LeaveGameNotificationDto) {
    this.sessionStore.dispatch(updateSession({ gameUpdate: lgn.gameUpdate }));
  }
}
