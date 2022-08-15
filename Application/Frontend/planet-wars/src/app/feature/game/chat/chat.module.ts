import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessageComponent } from './components/message/message.component';
import { MatCardModule } from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { ChatComponent } from './components/chat/chat.component';
import { chatReducer } from './state/chat.reducers';
import { StoreModule } from '@ngrx/store';
import { Features } from '../../features.enum';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [MessageComponent, ChatComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    StoreModule.forFeature(Features.Chat, chatReducer),
  ],
})
export class ChatModule {}
