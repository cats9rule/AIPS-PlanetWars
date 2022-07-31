import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionsConfirmationComponent } from './actions-confirmation.component';

describe('ActionsConfirmationComponent', () => {
  let component: ActionsConfirmationComponent;
  let fixture: ComponentFixture<ActionsConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActionsConfirmationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActionsConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
