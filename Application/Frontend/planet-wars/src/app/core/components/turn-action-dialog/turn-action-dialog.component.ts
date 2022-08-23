import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActionType } from 'core/enums/actionType.enum';
import { TurnActionDialogData } from 'core/interfaces/turn-action-dialog-data';

@Component({
  selector: 'app-turn-action-dialog',
  templateUrl: './turn-action-dialog.component.html',
  styleUrls: ['./turn-action-dialog.component.scss'],
})
export class TurnActionDialogComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: TurnActionDialogData,
    private dialogRef: MatDialogRef<TurnActionDialogComponent>
  ) {}

  public action = '';

  public armiesUsed = 0;
  public armiesLeft = 0;

  ngOnInit(): void {
    this.action = this.resolveAction(this.data.action);
    this.armiesLeft = this.data.availableArmies;
  }

  public onArmiesUsedChange(event: any) {
    const armies: number =
      event.target == null || event.target == undefined
        ? 0
        : event.target.value;
    if (armies > this.data.availableArmies)
      this.armiesUsed = this.data.availableArmies;
    else if (armies < 0) this.armiesUsed = 0;
  }

  public onAccept() {
    this.dialogRef.close(this.armiesUsed);
  }

  private resolveAction(type: ActionType) {
    switch (type) {
      case ActionType.Movement:
        return 'Moving Armies';
      case ActionType.Placement:
        return 'Placing Armies';
      case ActionType.Attack:
        return 'Attacking Planet';
    }
  }
}
