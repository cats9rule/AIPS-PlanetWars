import { TestBed } from '@angular/core/testing';

import { InitGameService } from './init-game.service';

describe('InitGameService', () => {
  let service: InitGameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InitGameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
