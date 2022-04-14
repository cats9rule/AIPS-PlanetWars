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
import { LoginService } from './login/login.service';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  imports: [
    HttpClientModule,
    BrowserModule,
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
  providers: [LoginService],
})
export class UserModule {}
