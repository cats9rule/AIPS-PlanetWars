import { Maybe } from 'src/app/core/utils/types/maybe.type';
import { User } from '../interfaces/user';

export interface UserState {
  loggedUser: Maybe<User>;
}

export const initialState: UserState = {
  loggedUser: null,
};
