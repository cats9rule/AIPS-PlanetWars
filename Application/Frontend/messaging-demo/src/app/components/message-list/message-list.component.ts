import { Component, Input, OnInit } from '@angular/core';
import { MessageDto } from 'src/interfaces/message-dto';

@Component({
  selector: 'app-message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.css']
})
export class MessageListComponent implements OnInit {

  @Input() messages: MessageDto[] | undefined;
  constructor() { }

  ngOnInit(): void {
  }

}
