import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { User } from 'src/app/feature/user/interfaces/user';
import { PlayerDto } from '../../../dtos/playerDto';
import { SessionDto } from '../../../dtos/sessionDto';
import { getPlayer, getSession } from '../../state/session.selectors';
import { SessionState } from '../../state/session.state';

@Component({
  selector: 'app-session-main',
  templateUrl: './session-main.component.html',
  styleUrls: ['./session-main.component.scss'],
})
export class SessionMainComponent implements OnInit, OnDestroy {
  private session$: Observable<Maybe<SessionDto>>;
  private sessionSubscription: Subscription = new Subscription();
  public session: Maybe<SessionDto>;

  public player$: Observable<Maybe<PlayerDto>>;

  public placingArmies = false;

  @Input()
  public user: Maybe<User>;

  constructor(private store: Store<SessionState>) {
    this.session$ = this.store.select<Maybe<SessionDto>>(getSession);
    this.player$ = this.store.select<Maybe<PlayerDto>>(getPlayer);
  }

  ngOnInit(): void {
    this.sessionSubscription = this.session$.subscribe({
      next: (sessionDto: Maybe<SessionDto>) => {
        if (isDefined(sessionDto)) {
          this.session = sessionDto;
        }
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  public setPlacingArmies(isPlacing: boolean) {
    this.placingArmies = isPlacing;
  }

  ngOnDestroy() {
    this.sessionSubscription.unsubscribe();
  }
}
