import { Injectable } from '@angular/core';
import { PlanetDto } from '../../dtos/planetDto';
import { AttackPlanetResource } from '../classes/attackPlanetResource';
import { BasicPlanet } from '../classes/basicPlanet';
import { DefensePlanetResource } from '../classes/defensePlanetResource';
import { MovementPlanetResource } from '../classes/movementPlanetResource';
import { Planet } from '../interfaces/planet';

@Injectable({
  providedIn: 'root',
})
export class PlanetBuilderService {
  private _planet: Planet;

  constructor() {
    this._planet = new BasicPlanet({
      id: '',
      ownerID: '',
      armyCount: 0,
      indexInGalaxy: -1,
      extras: '',
    });
  }

  public createPlanet(planet: PlanetDto): PlanetBuilderService {
    this._planet = new BasicPlanet(planet);
    return this;
  }

  public withMovementResource(): PlanetBuilderService {
    this._planet = new MovementPlanetResource(this._planet);
    return this;
  }

  public withAttackResource(): PlanetBuilderService {
    this._planet = new AttackPlanetResource(this._planet);
    return this;
  }

  public withDefenseResource(): PlanetBuilderService {
    this._planet = new DefensePlanetResource(this._planet);
    return this;
  }

  public build(): Planet {
    return this._planet;
  }
}
