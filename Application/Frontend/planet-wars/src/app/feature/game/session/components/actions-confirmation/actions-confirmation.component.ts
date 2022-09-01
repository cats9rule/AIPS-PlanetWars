import { outputAst } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TurnBuilderService } from '../../services/turn-builder.service';

@Component({
  selector: 'app-actions-confirmation',
  templateUrl: './actions-confirmation.component.html',
  styleUrls: ['./actions-confirmation.component.scss'],
})
export class ActionsConfirmationComponent implements OnInit {
  @Input()
  public isOnTurn = false;
  @Input()
  public notPlacedArmies = true;

  @Output()
  onDiscardMoveEvent = new EventEmitter<boolean>();
  @Output()
  onFinishMoveEvent = new EventEmitter<boolean>();

  constructor() {}

  ngOnInit(): void {}

  onFinishMove() {
    this.onFinishMoveEvent.emit(true);
  }

  onDiscard() {
    this.onDiscardMoveEvent.emit(true);
  }
}
