import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-session-info',
  templateUrl: './session-info.component.html',
  styleUrls: ['./session-info.component.scss'],
})
export class SessionInfoComponent implements OnInit {
  @Input()
  public sessionName = '';
  @Input()
  public totalPlanets = 0;

  constructor() {}

  ngOnInit(): void {}
}
