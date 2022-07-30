import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { UserModule } from './feature/user/user.module';
import { SnackbarMessageComponent } from './core/components/snackbar-message/snackbar-message.component';
import { CoreModule } from './core/core.module';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { LobbyModule } from './feature/game/lobby/lobby.module';
import { SessionModule } from './feature/game/session/session.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    StoreModule.forRoot({}, {}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      maxAge: 25, // Retains last 25 states
      autoPause: false,
    }),
    MatSidenavModule,
    MatSlideToggleModule,
    UserModule,
    CoreModule,
    LobbyModule,
    SessionModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
