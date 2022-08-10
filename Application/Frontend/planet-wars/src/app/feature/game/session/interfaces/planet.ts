import { PlanetMatrixCell } from './planetMatrixCell';
import { PlanetRenderInfo } from './planetRenderInfo';

export interface Planet {
  getAttack(): number;
  getDefense(): number;
  getMovement(): number;
  getPlanetRenderInfo(
    matrixCell: PlanetMatrixCell,
    ownerColor: string
  ): PlanetRenderInfo[];
}
