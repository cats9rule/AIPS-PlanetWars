import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
})
export class MenuComponent implements OnInit {
  @Output()
  public clickedButtonEvent = new EventEmitter<string>();

  constructor() {}

  ngOnInit(): void {}

  onCreateGame() {
    this.clickedButtonEvent.emit('createGame');
  }

  onJoinGame() {
    this.clickedButtonEvent.emit('joinGame');
  }
}
