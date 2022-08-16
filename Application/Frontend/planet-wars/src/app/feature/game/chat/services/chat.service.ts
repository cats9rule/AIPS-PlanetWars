import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { SnackbarMessage } from 'src/app/core/interfaces/snackbar-message';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { JoinSessionGroupDto } from '../../dtos/joinSessionGroupDto';
import { ClientHandlers } from '../../enums/clientHandlers.enum';
import { MessageHubService } from '../../services/message-hub.service';
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
