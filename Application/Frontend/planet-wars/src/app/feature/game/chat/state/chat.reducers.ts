import { createReducer, on } from '@ngrx/store';
import { disconnectChat, receiveChatMessage } from './chat.actions';
import { ChatState, initialChatState } from './chat.state';

export const chatReducer = createReducer(
  initialChatState,
  on(receiveChatMessage, (state: ChatState, { chatMessage }) => {
    const messages = state.messages;
    return {
      ...state,
      messages: [...messages, chatMessage],
    };
  }),
  on(disconnectChat, (state: ChatState) => {
    return {
      messages: [],
    };
  })
);
