import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap } from 'rxjs';
import { noAction } from 'src/app/core/state/common.actions';
import { MessageHubService } from '../../services/message-hub.service';
import { ChatService } from '../services/chat.service';
import { sendChatMessage } from './chat.actions';

@Injectable()
export class ChatEffects {
  constructor(
    private actions$: Actions,
    private chatService: ChatService,
    private hubService: MessageHubService
  ) {}

  sendChatMessage$ = createEffect(() =>
    this.actions$.pipe(
      ofType(sendChatMessage),
      mergeMap((action) => {
        this.chatService.sendMessage(action.messageDto);
        return [noAction()];
      })
    )
  );

  sendHubMessage$ = createEffect(() =>
    this.actions$.pipe(
      ofType(sendChatMessage),
      mergeMap((action) => {
        this.hubService.sendMessage(action.messageDto);
        return [noAction()];
      })
    )
  );
}
