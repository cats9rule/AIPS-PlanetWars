import { Injectable } from '@angular/core';
import { Maybe } from 'src/app/core/utils/types/maybe.type';
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

  constructor(private planetBuilder: PlanetBuilderService) {}

  public constructGalaxy(
    galaxy: Maybe<GalaxyDto>,
    matrixWidth: number,
    matrixHeight: number
  ) {
    if (galaxy != undefined) {
      const planets: Planet[] = [];
      const planetsRenderInfo: PlanetRenderInfo[] = [];

      galaxy.planets.forEach((planetDto) => {
        const planet = this.createPlanetFromDto(planetDto);
        planets.push(planet);
      });

      const cellWidth = (matrixWidth / (galaxy.gameMap.columns * 2 + 1)) * 2;
      const cellHeight = (matrixHeight / (galaxy.gameMap.rows * 2 + 1)) * 2;

      galaxy.gameMap.planetMatrix.forEach(
        (indicator: boolean, index: number) => {
          if (indicator == true) {
            const planet = planets[this.nextPlanetIndex++];
            const row = index / galaxy.gameMap.rows;
            const column = index % galaxy.gameMap.rows;

            const matCell: PlanetMatrixCell = {
              dx:
                row % 2 == 0
                  ? column * cellWidth
                  : column * cellWidth + cellWidth / 2,
              dy: row * cellHeight,
              x: cellWidth,
              y: cellHeight,
            };
            //TODO: resolve ownercolor
            const ownerColor = 'white';
            planetsRenderInfo.concat(
              planet.getPlanetRenderInfo(matCell, ownerColor)
            );
          }
        }
      );
      console.log(planets);
      console.log(planetsRenderInfo);
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

  private getPlanetRenderInfo(
    planet: Planet,
    matIndex: number,
    totalRows: number
  ) {}
}
