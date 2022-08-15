import { MessageDto } from '../dtos/messageDto';

export interface ChatState {
  messages: MessageDto[];
}

export const initialChatState: ChatState = {
  messages: [],
};
