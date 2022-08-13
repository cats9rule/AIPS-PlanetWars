import { Planet } from '../interfaces/planet';
import { PlanetMatrixCell } from '../interfaces/planetMatrixCell';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';
import { PlanetResource } from './planetResource';

export class AttackPlanetResource extends PlanetResource {
  private _attackBonus: number = 2;

  constructor(wrappee: Planet) {
    super(wrappee);
  }

  public override getAttack(): number {
    return this._wrappee.getAttack() * this._attackBonus;
  }

  public override getPlanetRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[] {
    console.log('Attack Planet render info');
    return this.getAttackRenderInfo(matrixCell, ownerColor).concat(
      this._wrappee.getPlanetRenderInfo(matrixCell, ownerColor)
    );
  }

  private getAttackRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[] {
    const x = matrixCell.x;
    const y = matrixCell.y;
    return [
      {
        cx: x / 2 + matrixCell.dx,
        cy: y / 2 + matrixCell.dy,
        indexInGalaxy: this.getIndexInGalaxy(),
        r: (x < y ? x : y) / 4 + 18,
        color: 'transparent',
        strokeDasharray: '10 5',
        strokeWidth: '1px',
        strokeColor: ownerColor,
      },
    ] as PlanetRenderInfo[];
  }
}
