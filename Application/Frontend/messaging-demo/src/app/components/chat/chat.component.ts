import { Component, Input, OnInit } from '@angular/core';
import { MessageDto } from 'src/interfaces/message-dto';
import { UserDto } from 'src/interfaces/user-dto';
import * as signalR from '@microsoft/signalr';
import { JoinGameChatDto } from 'src/interfaces/join-game-chat-dto';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit {
  @Input() user: UserDto | undefined;
  public joinedSession = false;
  public sessionID: string = '';
  public messages: MessageDto[] = [];
  public messageToSend: string = '';
  private hubConnection: signalR.HubConnection | undefined;

  constructor() {}

  ngOnInit(): void {}

  public receiveMessage(message: MessageDto) {
    
    
  }

  public async sendMessage() {

    if(this.user != undefined){
      const message: MessageDto = {
        clientHandler: 'receiveMessage',
        contents: this.messageToSend,
        sessionID: this.sessionID,
        userID: this.user.id,
        usernameWithTag: this.user.username + "#" + this.user.tag
      }
      console.log(message);
      console.log(await this.hubConnection?.invoke("SendChatMessage", message));
      this.messageToSend = "";
    }
  }

  public enterSession = async () => {
    
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chat')
      .build();

    this.hubConnection.on('receiveMessage', (message: MessageDto) => {
      console.log(message);
      this.messages.push(message);
    });

    this.hubConnection
      .start()
      .then(() => {
        if (this.user != undefined) {
          console.log('Connection started');
          const dto: JoinGameChatDto = {
            clientMethod: 'receiveMessage',
            sessionID: this.sessionID,
            userID: this.user.id,
            usernameWithTag: this.user.username + '#' + this.user.tag,
          };
          this.hubConnection?.invoke('JoinGameChat', dto);
          this.joinedSession = true;
        }
      })
      .catch((err) => console.log('Error while starting connection: ' + err));
    
  };

}
