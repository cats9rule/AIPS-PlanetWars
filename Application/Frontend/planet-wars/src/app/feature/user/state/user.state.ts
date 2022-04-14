import { Maybe } from 'src/app/core/utils/maybe.type';
import { User } from '../interfaces/user';

export interface UserState {
  loggedUser: Maybe<User>;
}

export const initialState: UserState = {
  loggedUser: null,
};
