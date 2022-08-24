import { Injectable } from '@angular/core';
import { ActionType } from '../../../../core/enums/actionType.enum';
import { TurnDto } from '../dtos/turnDto';

@Injectable({
  providedIn: 'root',
})
export class TurnBuilderService {
  private _turn: TurnDto;

  constructor() {
    this._turn = {
      actions: [],
      playerID: '',
      sessionID: '',
    };
  }

  public newTurn(playerID: string, sessionID: string): TurnBuilderService {
    this._turn = {
      actions: [],
      playerID: playerID,
      sessionID: sessionID,
    };
    return this;
  }

  public addMovementAction(
    planetFrom: string,
    planetTo: string,
    numberOfArmies: number
  ): TurnBuilderService {
    this._turn.actions.push({
      numberOfArmies,
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
      numberOfArmies,
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
      numberOfArmies,
      planetFrom: '',
      planetTo,
      playerID: this._turn.playerID,
      type: ActionType.Movement,
    });
    return this;
  }

  public build(): TurnDto {
    return this._turn;
  }
}
