import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { SessionDto } from '../../../dtos/sessionDto';
import { getSession } from '../../state/session.selectors';
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

  @Input()
  private userID: string = '';

  constructor(private store: Store<SessionState>) {
    this.session$ = this.store.select<Maybe<SessionDto>>(getSession);
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

  ngOnDestroy() {
    this.sessionSubscription.unsubscribe();
  }
}
