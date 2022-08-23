import { TestBed } from '@angular/core/testing';

import { TurnBuilderService } from './turn-builder.service';

describe('TurnBuilderService', () => {
  let service: TurnBuilderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TurnBuilderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
