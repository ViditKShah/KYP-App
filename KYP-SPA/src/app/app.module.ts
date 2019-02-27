import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { MentorsComponent } from './mentors/mentors.component';
import { MessagesComponent } from './messages/messages.component';
import { LikesComponent } from './likes/likes.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { CardsComponent } from './cards/cards.component';
import { DetailsComponent } from './details/details.component';

export function tokenGetter() {
   return localStorage.getItem('token');
}

const jwtConfig = {
   config: {
      tokenGetter: tokenGetter,
      whitelistedDomains: ['localhost:5000'],
      blacklistedRoutes: ['localhost:5000/api/auth']
   }
};

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      RegisterComponent,
      HomeComponent,
      MentorsComponent,
      MessagesComponent,
      LikesComponent,
      CardsComponent,
      DetailsComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot(jwtConfig)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      UserService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
