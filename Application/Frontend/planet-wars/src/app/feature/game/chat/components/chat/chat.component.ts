import { Component, Input, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { isDefined, Maybe } from 'core/utils/types/maybe.type';
import { User } from 'src/app/feature/user/interfaces/user';
import { MessageDto } from '../../dtos/messageDto';
import { sendChatMessage } from '../../state/chat.actions';
import { getChatMessages } from '../../state/chat.selectors';
import { ChatState } from '../../state/chat.state';
import 'extensions/string.extensions';
import { ClientHandlers } from 'core/enums/clientHandlers.enum';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {
  public messages$: Observable<MessageDto[]>;
  public isOpen = false;

  @Input()
  public user: Maybe<User>;
  @Input()
  public sessionID: string = '';

  public messageToSend: string = '';

  constructor(private store: Store<ChatState>) {
    this.messages$ = this.store.select<MessageDto[]>(getChatMessages);
  }

  ngOnInit(): void {}

  public sendMessage() {
    if (
      isDefined(this.user) // &&
      // !this.sessionID.isBlankOrEmpty() &&
      // !this.messageToSend.isBlankOrEmpty()
    ) {
      const messageDto: MessageDto = {
        clientHandler: ClientHandlers.receiveMessage,
        contents: this.messageToSend,
        sessionID: this.sessionID,
        userID: this.user!!.id,
        usernameWithTag: this.user!!.username + '#' + this.user!!.tag,
      };
      this.store.dispatch(sendChatMessage({ messageDto }));

      this.messageToSend = '';
    }
  }
}
