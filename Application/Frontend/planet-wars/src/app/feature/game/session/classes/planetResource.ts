import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { Planet } from '../interfaces/planet';
import { PlanetMatrixCell } from '../interfaces/planetMatrixCell';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';

export class PlanetResource implements Planet {
  protected _wrappee: Planet;

  constructor(wrappee: Planet) {
    this._wrappee = wrappee;
  }
  getArmyCount(): number {
    return this._wrappee.getArmyCount();
  }

  public getAttack(): number {
    return this._wrappee.getAttack();
  }

  public getDefense(): number {
    return this._wrappee.getDefense();
  }

  public getMovement(): number {
    return this._wrappee.getMovement();
  }

  public getID(): string {
    return this._wrappee.getID();
  }

  public getIndexInGalaxy(): number {
    return this._wrappee.getIndexInGalaxy();
  }

  public getOwnerID(): Maybe<string> {
    return this._wrappee.getOwnerID();
  }

  getPlanetRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[] {
    return [];
  }

  incrementArmyCount(diff: number): void {
    this._wrappee.incrementArmyCount(diff);
  }

  setOwnerID(id: string): void {
    this._wrappee.setOwnerID(id);
  }
}
