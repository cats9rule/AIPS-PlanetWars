import { createAction, props } from '@ngrx/store';
import { GalaxyDto } from '../../dtos/galaxyDto';
import { Planet } from '../interfaces/planet';
import { PlanetConnectionInfo } from '../interfaces/planetConnectionInfo';
import { PlanetRenderInfo } from '../interfaces/planetRenderInfo';

export const constructGalaxy = createAction(
  '[Galaxy] Construct Galaxy',
  props<{ galaxyDto: GalaxyDto; matrixWidth: number; matrixHeight: number }>()
);

export const constructGalaxySuccess = createAction(
  '[Galaxy] Construct Galaxy Success',
  props<{ planets: Planet[] }>()
);

export const constructPlanetRenderInfo = createAction(
  '[Galaxy] Construct Planet Render Info'
);

export const constructPlanetRenderInfoSuccess = createAction(
  '[Galaxy] Construct Planet Render Info Success',
  props<{ planetsRenderInfo: Map<number, PlanetRenderInfo[]> }>()
);

export const constructPlanetConnectionsRenderInfo = createAction(
  '[Galaxy] Construct Planet Connections Render Info'
);

export const constructPlanetConnectionsRenderInfoSuccess = createAction(
  '[Galaxy] Construct Planet Connections Render Info Success',
  props<{ connectionsRenderInfo: PlanetConnectionInfo[] }>()
);

export const updatePlanetOwner = createAction(
  '[Galaxy] Update Planet Owner',
  props<{ planetID: string; newOwnerID: string }>()
);
