import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { PlanetDto } from '../../dtos/planetDto';
import { Planet } from '../interfaces/planet';
import { PlanetMatrixCell } from '../interfaces/planetMatrixCell';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';

export class BasicPlanet implements Planet {
  private _id: string = '';
  private _ownerID: Maybe<string>;
  private _armyCount: number = 0;
  private _indexInGalaxy: number = -1;

  constructor(planetDto: PlanetDto) {
    this._id = planetDto.id;
    this._ownerID = planetDto.ownerID;
    this._armyCount = planetDto.armyCount;
    this._indexInGalaxy = planetDto.indexInGalaxy;
  }

  public getID(): string {
    return this._id;
  }

  public getOwnerID(): Maybe<string> {
    return this._ownerID;
  }
  public set ownerID(v: Maybe<string>) {
    this._ownerID = v;
  }

  public get armyCount(): number {
    return this._armyCount;
  }
  public set armyCount(v: number) {
    this._armyCount = v;
  }

  public getIndexInGalaxy(): number {
    return this._indexInGalaxy;
  }

  getAttack(): number {
    return 1;
  }

  getDefense(): number {
    return 1;
  }

  getMovement(): number {
    return 1;
  }

  getPlanetRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[] {
    console.log('Basic Planet render info');
    const x = matrixCell.x;
    const y = matrixCell.y;
    const renderInfo: PlanetRenderInfo = {
      cx: x / 2 + matrixCell.dx,
      cy: y / 2 + matrixCell.dy,
      indexInGalaxy: this._indexInGalaxy,
      r: (x < y ? x : y) / 4,
      color: ownerColor,
      strokeDasharray: '',
      strokeWidth: '2px',
      strokeColor: ownerColor,
    };
    return [renderInfo];
  }

  incrementArmyCount(diff: number): void {
    this._armyCount += diff;
  }

  setOwnerID(id: string): void {
    this._ownerID = id;
  }
}
