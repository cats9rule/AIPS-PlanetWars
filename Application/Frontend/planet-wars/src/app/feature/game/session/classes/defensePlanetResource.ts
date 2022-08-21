import { Planet } from '../interfaces/planet';
import { PlanetMatrixCell } from '../interfaces/planetMatrixCell';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';
import { PlanetResource } from './planetResource';

export class DefensePlanetResource extends PlanetResource {
  private _defenseBonus: number = 2;

  constructor(wrappee: Planet) {
    super(wrappee);
  }

  public override getDefense(): number {
    return this._wrappee.getDefense() * this._defenseBonus;
  }

  override getPlanetRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[] {
    return this.getDefenseRenderInfo(matrixCell, ownerColor).concat(
      this._wrappee.getPlanetRenderInfo(matrixCell, ownerColor)
    );
  }

  private getDefenseRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[] {
    const x = matrixCell.x;
    const y = matrixCell.y;
    const renderInfo1: PlanetRenderInfo = {
      cx: x / 2 + matrixCell.dx,
      cy: y / 2 + matrixCell.dy,
      indexInGalaxy: this.getIndexInGalaxy(),
      r: (x < y ? x : y) / 4 + 4,
      color: 'transparent',
      strokeDasharray: '',
      strokeWidth: '1px',
      strokeColor: ownerColor,
    };
    const renderInfo2: PlanetRenderInfo = {
      cx: x / 2 + matrixCell.dx,
      cy: y / 2 + matrixCell.dy,
      indexInGalaxy: this.getIndexInGalaxy(),
      r: (x < y ? x : y) / 4 + 6,
      color: 'transparent',
      strokeDasharray: '',
      strokeWidth: '1px',
      strokeColor: ownerColor,
    };
    return [renderInfo1, renderInfo2];
  }
}
