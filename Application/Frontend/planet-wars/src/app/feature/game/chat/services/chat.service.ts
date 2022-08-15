import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { SnackbarMessage } from 'src/app/core/interfaces/snackbar-message';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { ClientHandlers } from '../../enums/clientHandlers.enum';
import { MessageDto } from '../dtos/messageDto';
import { receiveChatMessage } from '../state/chat.actions';
import { ChatState } from '../state/chat.state';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private hubConnection: Maybe<signalR.HubConnection>;

  constructor(
    private store: Store<ChatState>,
    private snackbarService: SnackbarService
  ) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chat')
      .build();

    this.hubConnection.on(
      ClientHandlers.receiveMessage,
      (messageDto: MessageDto) => this.receiveMessage(messageDto)
    );

    this.hubConnection.start().catch((err) => {
      const msg: SnackbarMessage = {
        contents: `Cannot connect to chat service. Error: ${err}`,
        type: 'Error',
      };
      this.snackbarService.showMessage(msg, 'short');
    });
  }

  public async sendMessage(messageDto: MessageDto) {
    await this.hubConnection
      ?.invoke('SendChatMessage', messageDto)
      .catch((err) => {
        const msg: SnackbarMessage = {
          contents: `Cannot send message. Error: ${err}`,
          type: 'Error',
        };
        this.snackbarService.showMessage(msg, 'short');
      });
  }

  private receiveMessage(messageDto: MessageDto) {
    this.store.dispatch(receiveChatMessage({ chatMessage: messageDto }));
  }
}
