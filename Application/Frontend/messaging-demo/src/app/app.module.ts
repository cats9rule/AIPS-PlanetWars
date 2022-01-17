import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ChatComponent } from './components/chat/chat.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MessageComponent } from './components/message/message.component';
import {MatCardModule} from '@angular/material/card';
import { MessageListComponent } from './components/message-list/message-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ChatComponent,
    UserLoginComponent,
    MessageComponent,
    MessageListComponent
  ],
  imports: [
    BrowserModule,
    NoopAnimationsModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    HttpClientModule,
    MatCardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
