import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { PlayerStatsComponent } from './components/player-stats/player-stats.component';
import { SessionMainComponent } from './components/session-main/session-main.component';
import { PlayersInfoComponent } from './components/players-info/players-info.component';
import { ActionsComponent } from './components/actions/actions.component';
import { SessionInfoComponent } from './components/session-info/session-info.component';
import { sessionReducer } from './state/session.reducers';
import { StoreModule } from '@ngrx/store';
import { Features } from '../../features.enum';

@NgModule({
  declarations: [
    PlayerStatsComponent,
    SessionMainComponent,
    PlayersInfoComponent,
    ActionsComponent,
    SessionInfoComponent,
  ],
  imports: [
    CommonModule,
    StoreModule.forFeature(Features.Session, sessionReducer),
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatListModule,
  ],
  exports: [SessionMainComponent],
})
export class SessionModule {}
