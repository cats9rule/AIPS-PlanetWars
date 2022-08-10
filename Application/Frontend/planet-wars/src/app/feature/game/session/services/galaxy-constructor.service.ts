import { Injectable } from '@angular/core';
import { GalaxyDto } from '../../dtos/galaxyDto';
import { SessionModule } from '../session.module';

@Injectable({
  providedIn: 'root',
})
export class GalaxyConstructorService {
  constructor() {}

  public constructGalaxy(
    galaxy: GalaxyDto,
    matrixWidth: number,
    matrixHeight: number
  ) {}
}
