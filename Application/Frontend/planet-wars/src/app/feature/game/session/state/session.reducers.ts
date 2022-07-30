import { createReducer, on } from '@ngrx/store';
import { SessionDto } from '../../dtos/sessionDto';
import {
  createGameSuccess,
  joinGameSuccess,
} from '../../lobby/state/lobby.actions';
import { initialSessionState, SessionState } from './session.state';

export const sessionReducer = createReducer(
  initialSessionState,
  on(createGameSuccess, (state: SessionState, { sessionDto }) => {
    return setSession(state, sessionDto);
  }),
  on(joinGameSuccess, (state: SessionState, { sessionDto }) => {
    return setSession(state, sessionDto);
  })
);

const setSession = (state: SessionState, sessionDto: SessionDto) => {
  return {
    ...state,
    session: sessionDto,
  };
};
