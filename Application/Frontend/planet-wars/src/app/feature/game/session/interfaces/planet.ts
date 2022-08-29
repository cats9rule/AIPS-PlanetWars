import { Maybe } from 'src/app/core/utils/types/maybe.type';
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
  getIndexInGalaxy(): number;
  getOwnerID(): Maybe<string>;
  getID(): string;
  getArmyCount(): number;

  setOwnerID(id: string): void;
  incrementArmyCount(diff: number): void;
}
