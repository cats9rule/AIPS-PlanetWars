import { TestBed } from '@angular/core/testing';

import { PlanetBuilderService } from './planet-builder.service';

describe('PlanetBuilderService', () => {
  let service: PlanetBuilderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlanetBuilderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
