import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Store } from '@ngrx/store';
import { openInfoDialog } from 'core/state/dialog.actions';
import { SessionService } from '../../services/session.service';
import { SessionState } from '../../state/session.state';

@Component({
  selector: 'app-actions',
  templateUrl: './actions.component.html',
  styleUrls: ['./actions.component.scss'],
})
export class ActionsComponent implements OnInit {
  @Output()
  public onPlacingArmies = new EventEmitter<boolean>();
  @Output()
  public onMovingArmies = new EventEmitter<boolean>();
  @Output()
  public onAttackingPlanet = new EventEmitter<boolean>();

  @Input()
  public isOnTurn = false;
  @Input()
  public notPlacedArmies = true;

  constructor(
    private sessionStore: Store<SessionState>,
    private sessionService: SessionService
  ) {}

  ngOnInit(): void {}

  onPlaceArmies() {
    this.sessionStore.dispatch(
      openInfoDialog({
        data: {
          title: 'Placing armies',
          message: 'Choose a planet in your possession.',
        },
      })
    );
    //this.onPlacingArmies.emit(true);
    this.sessionService.messageSource.next(0);
  }
  onMoveArmies() {
    this.sessionStore.dispatch(
      openInfoDialog({
        data: {
          title: 'Moving armies',
          message:
            'How to move: first choose a start planet in your possession, ' +
            'then choose the destination planet in your possession. ' +
            'Lastly, choose the number of armies that you want to move.',
        },
      })
    );
    //this.onMovingArmies.emit(true);
    this.sessionService.messageSource.next(1);
  }
  onAttackPlanet() {
    this.sessionStore.dispatch(
      openInfoDialog({
        data: {
          title: 'Attacking a planet',
          message:
            'How to attack: first choose a start planet in your possession, ' +
            'then choose the destination planet not your possession. ' +
            'Lastly, choose the number of armies that you want to attack with.',
        },
      })
    );
    //this.onAttackingPlanet.emit(true);
    this.sessionService.messageSource.next(2);
  }

  resolveActionsEnabled() {
    if (this.isOnTurn) {
      if (!this.notPlacedArmies) {
        this.onPlacingArmies.emit(false);
        return true;
      }
    }
    return false;
  }
}
