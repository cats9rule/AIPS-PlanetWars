import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { Features } from '../features.enum';

import { LoginFormComponent } from './login/login-form/login-form.component';
import { userReducer } from './state/user.reducers';

import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  imports: [
    StoreModule.forFeature(Features.User, userReducer),
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
  ],
  exports: [],
  declarations: [LoginFormComponent],
  providers: [],
})
export class UserModule {}
