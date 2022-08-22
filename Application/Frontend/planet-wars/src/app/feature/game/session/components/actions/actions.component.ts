import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { placingArmies } from '../../state/session.actions';
import { SessionState } from '../../state/session.state';

@Component({
  selector: 'app-actions',
  templateUrl: './actions.component.html',
  styleUrls: ['./actions.component.scss'],
})
export class ActionsComponent implements OnInit {
  public movingArmies = false;
  public attackingPlanet = false;

  public notPlacedArmies = true;

  constructor(private sessionStore: Store<SessionState>) {}

  ngOnInit(): void {}

  onPlaceArmies() {
    this.sessionStore.dispatch(placingArmies({ placingArmies: true }));
  }
  onMoveArmies() {
    this.movingArmies = true;
  }
  onAttackPlanet() {
    this.attackingPlanet = true;
  }
}
