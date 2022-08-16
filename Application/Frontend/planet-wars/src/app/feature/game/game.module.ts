import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LobbyModule } from './lobby/lobby.module';
import { SessionModule } from './session/session.module';
import { ChatModule } from './chat/chat.module';

@NgModule({
  declarations: [],
  imports: [CommonModule, LobbyModule, SessionModule, ChatModule],
  exports: [],
})
export class GameModule {}
