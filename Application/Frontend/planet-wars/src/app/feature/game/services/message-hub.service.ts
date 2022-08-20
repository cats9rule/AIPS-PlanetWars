import { Injectable } from '@angular/core';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import * as signalR from '@microsoft/signalr';
import { User } from '../../user/interfaces/user';
import { ChatState } from '../chat/state/chat.state';
import { Store } from '@ngrx/store';
import { SnackbarService } from 'src/app/core/utils/services/snackbar.service';
import { JoinSessionGroupDto } from '../dtos/joinSessionGroupDto';
import { SnackbarMessage } from 'src/app/core/interfaces/snackbar-message';
import { MessageDto } from '../chat/dtos/messageDto';
import { ChatService } from '../chat/services/chat.service';
import { ClientHandlers } from '../enums/clientHandlers.enum';
import { SessionService } from '../session/services/session.service';
import { NewPlayerDto } from '../dtos/newPlayerDto';

@Injectable({
  providedIn: 'root',
})
export class MessageHubService {
  private hubConnection: Maybe<signalR.HubConnection>;
  constructor(
    private chatService: ChatService,
    private sessionService: SessionService,
    private snackbarService: SnackbarService
  ) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/GameUpdates')
      .build();
  }

  public startHubConnection(user: Maybe<User>, sessionID: string) {
    if (isDefined(this.hubConnection)) {
      this.setClientHandlers();

      this.hubConnection!!.start()
        .then(() => {
          if (user != undefined) {
            console.log('Connection started');
            const dto: JoinSessionGroupDto = {
              clientHandler: ClientHandlers.receiveMessage,
              sessionID: sessionID,
              userID: user.id,
              usernameWithTag: user.username + '#' + user.tag,
            };
            this.hubConnection?.invoke('JoinGameGroup', dto);
          }
        })
        .catch((err) => {
          const msg: SnackbarMessage = {
            contents: `Cannot connect to hub service. ${err}`,
            type: 'Error',
          };
          this.snackbarService.showMessage(msg, 'long');
        });
    }
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

  private setClientHandlers() {
    this.hubConnection!!.on(
      ClientHandlers.receiveMessage,
      (messageDto: MessageDto) => this.receiveMessage(messageDto)
    );

    this.hubConnection!!.on(
      ClientHandlers.onNewPlayer,
      (newPlayerDto: NewPlayerDto) => {
        console.log('ON NEW PLAYER');
        this.sessionService.addPlayer(newPlayerDto.newPlayer);
      }
    );
  }

  private receiveMessage(messageDto: MessageDto) {
    this.chatService.receiveMessage(messageDto);
  }
}