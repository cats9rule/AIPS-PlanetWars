import { createReducer, on } from '@ngrx/store';
import { receiveChatMessage } from './chat.actions';
import { ChatState, initialChatState } from './chat.state';

export const chatReducer = createReducer(
  initialChatState,
  on(receiveChatMessage, (state: ChatState, { chatMessage }) => {
    const messages = state.messages;
    messages.push(chatMessage);
    return {
      ...state,
      messages: messages,
    };
  })
);
