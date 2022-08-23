import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Store } from '@ngrx/store';
import { placingArmies } from '../../state/session.actions';
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

  public notPlacedArmies = true;

  constructor(private sessionStore: Store<SessionState>) {}

  ngOnInit(): void {}

  onPlaceArmies() {
    //this.sessionStore.dispatch(placingArmies({ placingArmies: true }));
    this.onPlacingArmies.emit(true);
  }
  onMoveArmies() {
    //this.movingArmies = true;
  }
  onAttackPlanet() {
    //this.attackingPlanet = true;
  }
}
