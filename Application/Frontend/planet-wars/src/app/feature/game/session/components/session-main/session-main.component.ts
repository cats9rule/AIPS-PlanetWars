import { ThisReceiver } from '@angular/compiler';
import {
  AfterViewInit,
  Component,
  Input,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { SnackbarService } from 'core/utils/services/snackbar.service';
import { Observable, Subscription } from 'rxjs';
import { isDefined, Maybe } from 'src/app/core/utils/types/maybe.type';
import { User } from 'src/app/feature/user/interfaces/user';
import { PlayerDto } from '../../../dtos/playerDto';
import { SessionDto } from '../../../dtos/sessionDto';
import { TurnBuilderService } from '../../services/turn-builder.service';
import { setSessionState } from '../../state/session.actions';
import {
  getPlayer,
  getSession,
  getSessionState,
} from '../../state/session.selectors';
import { initialSessionState, SessionState } from '../../state/session.state';

@Component({
  selector: 'app-session-main',
  templateUrl: './session-main.component.html',
  styleUrls: ['./session-main.component.scss'],
})
export class SessionMainComponent implements OnInit, OnDestroy, AfterViewInit {
  private session$: Observable<Maybe<SessionDto>>;
  private sessionSubscription: Subscription = new Subscription();
  public session: Maybe<SessionDto>;

  public player$: Observable<Maybe<PlayerDto>>;

  private sessionState$: Observable<SessionState>;
  private sessionStateSubscription: Subscription = new Subscription();
  public sessionState: SessionState = initialSessionState;

  public placingArmies = false;
  public notPlacedArmies = false;

  @Input()
  public user: Maybe<User>;

  constructor(
    private store: Store<SessionState>,
    private turnBuilder: TurnBuilderService,
    private snackbarService: SnackbarService
  ) {
    this.session$ = this.store.select<Maybe<SessionDto>>(getSession);
    this.player$ = this.store.select<Maybe<PlayerDto>>(getPlayer);
    this.sessionState$ = this.store.select<SessionState>(getSessionState);
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
    this.sessionStateSubscription = this.sessionState$.subscribe({
      next: (state) => {
        this.sessionState = state;
        if (state.armiesToPlace > 0) {
          this.notPlacedArmies = true;
        } else {
          this.notPlacedArmies = false;
        }
      },
    });
  }

  ngAfterViewInit(): void {
    this.turnBuilder.newTurn(this.sessionState);
  }

  public setPlacingArmies(isPlacing: boolean) {
    this.placingArmies = isPlacing;
  }

  public onFinishMove() {
    //TODO: dispatch action for sending turn info to server
  }

  public onDiscardMove() {
    const oldSessionState = this.turnBuilder.discard();
    this.store.dispatch(setSessionState({ sessionState: oldSessionState }));
    this.snackbarService.showMessage(
      { type: 'Warning', contents: 'All actions have been discarded.' },
      'short'
    );
  }

  ngOnDestroy() {
    this.sessionSubscription.unsubscribe();
    this.sessionStateSubscription.unsubscribe();
  }
}
