import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Features } from 'src/app/feature/features.enum';
import { ChatState } from './chat.state';

export const getChatState = createFeatureSelector<ChatState>(Features.Chat);

export const getChatMessages = createSelector(
  getChatState,
  (state: ChatState) => state.messages
);
