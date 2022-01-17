import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {
  @Input() usernameWithTag: string | undefined;
  @Input() contents: string | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
