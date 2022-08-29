import { Injectable } from '@angular/core';
import { ActionType } from '../../../../core/enums/actionType.enum';
import { TurnDto } from '../dtos/turnDto';
import { initialSessionState, SessionState } from '../state/session.state';

@Injectable({
  providedIn: 'root',
})
export class TurnBuilderService {
  private _turn: TurnDto;

  private _backupSessionState = initialSessionState;

  constructor() {
    this._turn = {
      actions: [],
      playerID: '',
      sessionID: '',
    };
  }

  public newTurn(sessionState: SessionState): TurnBuilderService {
    this._backupSessionState = sessionState;
    this._turn = {
      actions: [],
      playerID: sessionState.player!!.id,
      sessionID: sessionState.session!!.id,
    };
    return this;
  }

  public addMovementAction(
    planetFrom: string,
    planetTo: string,
    numberOfArmies: number
  ): TurnBuilderService {
    this._turn.actions.push({
      armies: numberOfArmies,
      planetFrom,
      planetTo,
      playerID: this._turn.playerID,
      type: ActionType.Movement,
    });
    return this;
  }

  public addAttackAction(
    planetFrom: string,
    planetTo: string,
    numberOfArmies: number
  ): TurnBuilderService {
    this._turn.actions.push({
      armies: numberOfArmies,
      planetFrom,
      planetTo,
      playerID: this._turn.playerID,
      type: ActionType.Attack,
    });
    return this;
  }

  public addPlacementAction(
    planetTo: string,
    numberOfArmies: number
  ): TurnBuilderService {
    this._turn.actions.push({
      armies: numberOfArmies,
      planetFrom: '',
      planetTo,
      playerID: this._turn.playerID,
      type: ActionType.Placement,
    });
    return this;
  }

  public build(): TurnDto {
    return this._turn;
  }

  public discard(): SessionState {
    this.newTurn(this._backupSessionState);
    return this._backupSessionState;
  }
}
