import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TurnActionDialogComponent } from './turn-action-dialog.component';

describe('TurnActionDialogComponent', () => {
  let component: TurnActionDialogComponent;
  let fixture: ComponentFixture<TurnActionDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TurnActionDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TurnActionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
