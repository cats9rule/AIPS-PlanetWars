import { TestBed } from '@angular/core/testing';

import { GalaxyConstructorService } from './galaxy-constructor.service';

describe('GalaxyConstructorService', () => {
  let service: GalaxyConstructorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GalaxyConstructorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
