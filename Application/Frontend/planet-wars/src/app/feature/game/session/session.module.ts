import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { PlayerStatsComponent } from './components/player-stats/player-stats.component';
import { SessionMainComponent } from './components/session-main/session-main.component';
import { PlayersInfoComponent } from './components/players-info/players-info.component';
import { ActionsComponent } from './components/actions/actions.component';
import { SessionInfoComponent } from './components/session-info/session-info.component';
import { sessionReducer } from './state/session.reducers';
import { StoreModule } from '@ngrx/store';
import { Features } from '../../features.enum';
import { ActionsConfirmationComponent } from './components/actions-confirmation/actions-confirmation.component';
import { GalaxyComponent } from './components/galaxy/galaxy.component';
import { GalaxyConstructorService } from './services/galaxy-constructor.service';
import { EffectsModule } from '@ngrx/effects';
import { SessionEffects } from './state/session.effects';
import { ChatModule } from '../chat/chat.module';

@NgModule({
  declarations: [
    PlayerStatsComponent,
    SessionMainComponent,
    PlayersInfoComponent,
    ActionsComponent,
    SessionInfoComponent,
    ActionsConfirmationComponent,
    GalaxyComponent,
  ],
  imports: [
    CommonModule,
    StoreModule.forFeature(Features.Session, sessionReducer),
    EffectsModule.forFeature([SessionEffects]),
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatListModule,
    MatGridListModule,
    ChatModule,
  ],
  exports: [SessionMainComponent],
})
export class SessionModule {}
