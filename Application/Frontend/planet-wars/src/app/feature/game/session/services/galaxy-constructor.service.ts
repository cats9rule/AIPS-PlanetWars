import { Injectable } from '@angular/core';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { GalaxyDto } from '../../dtos/galaxyDto';
import { GameMapDto } from '../../dtos/gameMapDto';
import { PlanetDto } from '../../dtos/planetDto';
import { PlanetResources } from '../enums/planetResources';
import { Planet } from '../interfaces/planet';
import { PlanetMatrixCell } from '../interfaces/planetMatrixCell';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';
import { PlanetBuilderService } from './planet-builder.service';

@Injectable({
  providedIn: 'root',
})
export class GalaxyConstructorService {
  private nextPlanetIndex = 0;

  private planets: Planet[] = [];
  private planetsRenderInfo: Map<number, PlanetRenderInfo[]> = new Map();

  private _matrixWidth = 0;
  private _matrixHeight = 0;
  private _gameMap: Maybe<GameMapDto>;

  constructor(private planetBuilder: PlanetBuilderService) {}

  public constructGalaxy(
    galaxy: Maybe<GalaxyDto>,
    matrixWidth: number,
    matrixHeight: number
  ) {
    if (galaxy != undefined) {
      const planets: Planet[] = [];

      galaxy.planets.forEach((planetDto) => {
        const planet = this.createPlanetFromDto(planetDto);
        planets.push(planet);
      });

      this.planets = planets;

      this._gameMap = galaxy.gameMap;
      this._matrixHeight = matrixHeight;
      this._matrixWidth = matrixWidth;

      this.getRenderInfoForGalaxy();
      console.log(this.planets);
      console.log(this.planetsRenderInfo);
    }
  }

  public getRenderInfoForGalaxy() {
    if (isDefined(this._gameMap)) {
      this._gameMap!!.planetMatrix.forEach(
        (indicator: boolean, index: number) => {
          if (indicator == true) {
            const planet = this.planets[this.nextPlanetIndex++];
            const matCell = this.getMatrixCellInfo(index);
            const ownerColor = this.resolveOwnerColor(planet.getOwnerID());
            this.planetsRenderInfo.set(
              planet.getIndexInGalaxy(),
              planet.getPlanetRenderInfo(matCell, ownerColor)
            );
            console.log(
              `Planet ${
                planet.getIndexInGalaxy() + 1
              } attack: ${planet.getAttack()}, defense: ${planet.getDefense()}, movement: ${planet.getMovement()}`
            );
          }
        }
      );
    }
  }

  public updatePlanetOwnership(planetIndex: number, ownerID: string): void {
    if (planetIndex >= 0) {
      this.planets[planetIndex].setOwnerID(ownerID);
      //TODO: summon upon the server!
      this.planetsRenderInfo.set(
        planetIndex,
        this.planets[planetIndex].getPlanetRenderInfo(
          this.getMatrixCellInfo(planetIndex),
          this.resolveOwnerColor(ownerID)
        )
      );
    }
  }

  public updatePlanetArmyCount(planetIndex: number, armyDiff: number): void {
    if (planetIndex >= 0) {
      this.planets[planetIndex].incrementArmyCount(armyDiff);
      //TODO: summon upon the server!
    }
  }

  private createPlanetFromDto(planetDto: PlanetDto): Planet {
    this.planetBuilder.createPlanet(planetDto);
    if (planetDto.extras.search(PlanetResources.attack) != -1) {
      this.planetBuilder.withAttackResource();
    }
    if (planetDto.extras.search(PlanetResources.defense) != -1) {
      this.planetBuilder.withDefenseResource();
    }
    if (planetDto.extras.search(PlanetResources.movement) != -1) {
      this.planetBuilder.withMovementResource();
    }
    return this.planetBuilder.build();
  }

  private getMatrixCellInfo(index: number): PlanetMatrixCell {
    const row = index / this._gameMap!!.rows;
    const column = index % this._gameMap!!.rows;
    const cellWidth =
      (this._matrixWidth / (this._gameMap!!.columns * 2 + 1)) * 2;
    const cellHeight =
      (this._matrixHeight / (this._gameMap!!.rows * 2 + 1)) * 2;

    return {
      dx:
        row % 2 == 0 ? column * cellWidth : column * cellWidth + cellWidth / 2,
      dy: row * cellHeight,
      x: cellWidth,
      y: cellHeight,
    } as PlanetMatrixCell;
  }

  private resolveOwnerColor(ownerID: Maybe<string>): string {
    return 'white';
    //TODO: implement
  }
}
