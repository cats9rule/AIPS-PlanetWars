import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateGameFormComponent } from './components/create-game-form/create-game-form.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { CoreModule } from 'src/app/core/core.module';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { Features } from '../../features.enum';
import { lobbyReducer } from './state/lobby.reducers';
import { MatTableModule } from '@angular/material/table';
import { EffectsModule } from '@ngrx/effects';
import { LobbyEffects } from './state/lobby.effects';
import { HomeComponent } from './components/home/home.component';
import { MenuComponent } from './components/menu/menu.component';
import { JoinGameFormComponent } from './components/join-game-form/join-game-form.component';

@NgModule({
  declarations: [CreateGameFormComponent, HomeComponent, MenuComponent, JoinGameFormComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    BrowserModule,
    StoreModule.forFeature(Features.Lobby, lobbyReducer),
    EffectsModule.forFeature([LobbyEffects]),
    CoreModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatExpansionModule,
    MatTableModule,
  ],
  exports: [HomeComponent],
})
export class LobbyModule {}
