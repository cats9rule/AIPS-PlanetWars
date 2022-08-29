import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Store } from '@ngrx/store';
import { openInfoDialog } from 'core/state/dialog.actions';
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
  public notPlacedArmies = true;

  constructor(private sessionStore: Store<SessionState>) {}

  ngOnInit(): void {}

  onPlaceArmies() {
    //this.sessionStore.dispatch(placingArmies({ placingArmies: true }));
    this.sessionStore.dispatch(
      openInfoDialog({
        data: {
          title: 'Placing armies',
          message: 'Choose a planet in your possession.',
        },
      })
    );
    this.onPlacingArmies.emit(true);
  }
  onMoveArmies() {
    //this.movingArmies = true;
  }
  onAttackPlanet() {
    //this.attackingPlanet = true;
  }
}
