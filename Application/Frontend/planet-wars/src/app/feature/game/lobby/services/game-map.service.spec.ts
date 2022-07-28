import { TestBed } from '@angular/core/testing';

import { GameMapService } from './game-map.service';

describe('GameMapService', () => {
  let service: GameMapService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GameMapService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
