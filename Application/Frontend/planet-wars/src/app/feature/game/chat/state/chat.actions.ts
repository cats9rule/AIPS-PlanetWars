import { createAction, props } from '@ngrx/store';
import { MessageDto } from '../dtos/messageDto';

export const receiveChatMessage = createAction(
  '[Chat] Receive Chat Message',
  props<{ chatMessage: MessageDto }>()
);

export const sendChatMessage = createAction(
  '[Chat] Send Chat Message',
  props<{ messageDto: MessageDto }>()
);

export const sendHubMessage = createAction(
  '[Message Hub] Send Hub Message',
  props<{ messageDto: MessageDto }>()
);
