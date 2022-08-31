import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { GalaxyDto } from '../../dtos/galaxyDto';
import { GameMapDto } from '../../dtos/gameMapDto';
import { PlanetDto } from '../../dtos/planetDto';
import { PlayerDto } from '../../dtos/playerDto';
import { PlanetResources } from '../enums/planetResources';
import { Planet } from '../interfaces/planet';
import { PlanetConnectionInfo } from '../interfaces/planetConnectionInfo';
import { PlanetMatrixCell } from '../interfaces/planetMatrixCell';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';
import { getAllPlayers, getPlanets } from '../state/session.selectors';
import { SessionState } from '../state/session.state';
import { PlanetBuilderService } from './planet-builder.service';

@Injectable({
  providedIn: 'root',
})
export class GalaxyConstructorService implements OnDestroy {
  private nextPlanetIndex = 0;

  private planets: Planet[] = [];
  private planetsRenderInfo: PlanetRenderInfo[] = [];
  private connectionsRenderInfo: PlanetConnectionInfo[] = [];

  private _matrixWidth = 0;
  private _matrixHeight = 0;
  private _gameMap: Maybe<GameMapDto>;

  private players: PlayerDto[] = [];
  private players$: Observable<Maybe<PlayerDto[]>>;
  private playerSubscription = new Subscription();

  private statePlanets: Planet[] = [];
  private statePlanets$: Observable<Maybe<Planet[]>>;
  private statePlanetsSubscription = new Subscription();

  constructor(
    private planetBuilder: PlanetBuilderService,
    private sessionStore: Store<SessionState>
  ) {
    this.players$ = this.sessionStore.select<Maybe<PlayerDto[]>>(getAllPlayers);
    this.playerSubscription = this.players$.subscribe({
      next: (players) => {
        if (isDefined(players)) {
          this.players = players!!;
        }
      },
    });

    this.statePlanets$ = this.sessionStore.select<Planet[]>(getPlanets);
    this.statePlanetsSubscription = this.statePlanets$.subscribe({
      next: (planets) => {
        if (isDefined(planets)) {
          this.statePlanets = planets!!;
        }
      },
    });
  }

  public constructGalaxy(
    galaxy: Maybe<GalaxyDto>,
    matrixWidth: number,
    matrixHeight: number
  ): Planet[] {
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

      return this.planets;
    }
    return [];
  }

  public getRenderInfoForGalaxy() {
    if (isDefined(this._gameMap)) {
      this.planetsRenderInfo = [];
      this._gameMap!!.planetMatrix.forEach(
        (indicator: boolean, index: number) => {
          if (indicator == true) {
            const planet = this.statePlanets[this.nextPlanetIndex++];
            const matCell = this.getMatrixCellInfo(index);
            const ownerColor = this.resolveOwnerColor(planet.getOwnerID());
            this.planetsRenderInfo = this.planetsRenderInfo.concat(
              planet.getPlanetRenderInfo(matCell, ownerColor)
            );
          }
        }
      );
      this.nextPlanetIndex = 0;
    }
    return this.planetsRenderInfo;
  }

  public getConnectionsRenderInfo() {
    if (isDefined(this._gameMap)) {
      const connections: PlanetConnectionInfo[] = [];
      let renderInfos: PlanetRenderInfo[] = [];
      this.planetsRenderInfo.forEach((value) => {
        renderInfos = renderInfos.concat(value);
      });
      const graph = this._gameMap!!.planetGraph;

      for (const key in graph) {
        const planetFrom = renderInfos.find(
          (planet) => planet.indexInGalaxy.toString() == key
        );
        if (planetFrom != undefined) {
          const value = graph[key];
          value!!.forEach((connectedPlanetIndex: number) => {
            const planetTo = renderInfos.find(
              (planet) => planet.indexInGalaxy == connectedPlanetIndex
            );
            if (planetTo != undefined) {
              const connection: PlanetConnectionInfo = {
                x1: planetFrom.cx,
                y1: planetFrom.cy,
                x2: planetTo.cx,
                y2: planetTo.cy,
                color: 'white',
                strokeDasharray: '2',
                strokeWidth: '2px',
                identifier:
                  planetFrom.indexInGalaxy.toString() +
                  planetTo.indexInGalaxy.toString(),
              };
              if (
                connections.find(
                  (c) =>
                    c.identifier ==
                      planetFrom.indexInGalaxy.toString() +
                        planetTo.indexInGalaxy.toString() ||
                    c.identifier ==
                      planetTo.indexInGalaxy.toString() +
                        planetFrom.indexInGalaxy.toString()
                ) == undefined
              ) {
                connections.push(connection);
              }
            }
          });
        }
      }
      this.connectionsRenderInfo = connections;

      return this.connectionsRenderInfo;
    }
    return [];
  }

  public updatePlanetOwnership(planetIndex: number, ownerID: string): void {
    if (planetIndex >= 0) {
      this.planets[planetIndex].setOwnerID(ownerID);
      this.planetsRenderInfo = this.planetsRenderInfo.concat(
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
    }
  }

  public createPlanetFromDto(planetDto: PlanetDto): Planet {
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
    const row = Math.floor(index / this._gameMap!!.columns);
    const column = index % this._gameMap!!.columns;
    const cellWidth =
      (this._matrixWidth / (this._gameMap!!.columns * 2 + 1)) * 2;
    const cellHeight = this._matrixHeight / this._gameMap!!.rows;

    return {
      dx:
        row % 2 == 0 ? column * cellWidth : column * cellWidth + cellWidth / 2,
      dy: row * cellHeight,
      x: cellWidth,
      y: cellHeight,
    } as PlanetMatrixCell;
  }

  private resolveOwnerColor(ownerID: Maybe<string>): string {
    const owner = this.players.find((p) => p.id == ownerID);
    if (owner != undefined) {
      return owner.playerColor;
    }
    return 'white';
  }

  ngOnDestroy(): void {
    this.playerSubscription.unsubscribe();
    this.statePlanetsSubscription.unsubscribe();
  }
}
