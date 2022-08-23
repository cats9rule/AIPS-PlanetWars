import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { MessageDto } from '../dtos/messageDto';
import { receiveChatMessage, sendHubMessage } from '../state/chat.actions';
import { ChatState } from '../state/chat.state';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  constructor(private store: Store<ChatState>) {}

  public async sendMessage(messageDto: MessageDto) {
    this.store.dispatch(sendHubMessage({ messageDto }));
    //this.hubService.sendMessage(messageDto);
  }

  public receiveMessage(messageDto: MessageDto) {
    this.store.dispatch(receiveChatMessage({ chatMessage: messageDto }));
  }
}
