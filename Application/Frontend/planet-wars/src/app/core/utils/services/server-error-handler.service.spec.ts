import { TestBed } from '@angular/core/testing';

import { ServerErrorHandlerService } from './server-error-handler.service';

describe('ServerErrorHandlerService', () => {
  let service: ServerErrorHandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServerErrorHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
