import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LobbyModule } from './lobby/lobby.module';
import { SessionModule } from './session/session.module';
import { ChatModule } from './chat/chat.module';
import { SessionMainComponent } from './session/components/session-main/session-main.component';
import { HomeComponent } from './lobby/components/home/home.component';
import { ChatComponent } from './chat/components/chat/chat.component';

@NgModule({
  declarations: [],
  imports: [CommonModule, LobbyModule, SessionModule, ChatModule],
  exports: [SessionMainComponent, HomeComponent, ChatComponent],
})
export class GameModule {}
