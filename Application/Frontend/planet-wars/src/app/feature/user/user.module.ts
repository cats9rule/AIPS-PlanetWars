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
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { CoreModule } from 'src/app/core/core.module';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from './state/user.effects';
import { AuthorizeComponent } from './authorize/authorize.component';
import { SignupFormComponent } from './signup/signup-form/signup-form.component';

@NgModule({
  imports: [
    HttpClientModule,
    BrowserModule,
    StoreModule.forFeature(Features.User, userReducer),
    EffectsModule.forFeature([UserEffects]),
    CoreModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
  ],
  exports: [LoginFormComponent, AuthorizeComponent, SignupFormComponent],
  declarations: [LoginFormComponent, AuthorizeComponent, SignupFormComponent],
  providers: [],
})
export class UserModule {}
