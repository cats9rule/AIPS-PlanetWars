import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinGameFormComponent } from './join-game-form.component';

describe('JoinGameFormComponent', () => {
  let component: JoinGameFormComponent;
  let fixture: ComponentFixture<JoinGameFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JoinGameFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JoinGameFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
