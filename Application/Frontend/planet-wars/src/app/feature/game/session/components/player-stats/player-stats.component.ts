import {
  ChangeDetectionStrategy,
  Component,
  Input,
  OnInit,
} from '@angular/core';

@Component({
  selector: 'app-player-stats',
  templateUrl: './player-stats.component.html',
  styleUrls: ['./player-stats.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PlayerStatsComponent implements OnInit {
  @Input()
  public planetCount = 0;
  @Input()
  public armyCount = 0;
  @Input()
  public armiesNextTurn = 0;

  constructor() {}

  ngOnInit(): void {}
}
