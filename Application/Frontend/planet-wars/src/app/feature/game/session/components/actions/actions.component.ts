import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-actions',
  templateUrl: './actions.component.html',
  styleUrls: ['./actions.component.scss'],
})
export class ActionsComponent implements OnInit {
  public placingArmies = false;
  public movingArmies = false;
  public attackingPlanet = false;

  constructor() {}

  ngOnInit(): void {}

  onPlaceArmies() {
    this.placingArmies = true;
  }
  onMoveArmies() {
    this.movingArmies = true;
  }
  onAttackPlanet() {
    this.attackingPlanet = true;
  }
}
