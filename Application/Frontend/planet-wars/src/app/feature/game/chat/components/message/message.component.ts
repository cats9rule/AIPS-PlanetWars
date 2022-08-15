import { Component, Input, OnInit } from '@angular/core';
import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { MessageDto } from '../../dtos/messageDto';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss'],
})
export class MessageComponent implements OnInit {
  @Input()
  public messageDto: Maybe<MessageDto>;

  constructor() {}

  ngOnInit(): void {}
}
