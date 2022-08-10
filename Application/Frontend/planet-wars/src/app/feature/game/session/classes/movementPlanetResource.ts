import { Planet } from '../interfaces/planet';
import { PlanetMatrixCell } from '../interfaces/planetMatrixCell';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';
import { PlanetResource } from './planetResource';

export class MovementPlanetResource extends PlanetResource {
  private _movementBonus: number = 2;

  constructor(wrappee: Planet) {
    super(wrappee);
  }

  public override getMovement(): number {
    return this._wrappee.getMovement() * this._movementBonus;
  }

  public override getPlanetRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[] {
    return this.getMovementRenderInfo(matrixCell, ownerColor).concat(
      this._wrappee.getPlanetRenderInfo(matrixCell, ownerColor)
    );
  }

  private getMovementRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[] {
    const x = matrixCell.x;
    const y = matrixCell.y;
    return [
      {
        cx: x / 2 + matrixCell.dx,
        cy: y / 2 + matrixCell.dy,
        indexInGalaxy: -1,
        r: (x < y ? x : y) / 4 + 8,
        color: 'transparent',
        strokeDasharray: '5',
        strokeWidth: '1px',
        strokeColor: ownerColor,
      },
    ] as PlanetRenderInfo[];
  }
}
