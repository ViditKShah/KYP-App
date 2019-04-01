import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { TimeAgoPipe } from 'time-ago-pipe';

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
import { MentorDetailResolver } from './_resolvers/mentor-detail.resolver';
import { MentorListResolver } from './_resolvers/mentor-list.resolver';
import { EditComponent } from './edit/edit.component';
import { MentorEditResolver } from './_resolvers/mentor-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { PhotosComponent } from './photos/photos.component';
import { ListsResolver } from './_resolvers/lists.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { ThreadsComponent } from './threads/threads.component';

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
      DetailsComponent,
      EditComponent,
      PhotosComponent,
      TimeAgoPipe,
      ThreadsComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      PaginationModule.forRoot(),
      TabsModule.forRoot(),
      ButtonsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      JwtModule.forRoot(jwtConfig),
      FileUploadModule
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      UserService,
      MentorDetailResolver,
      MentorListResolver,
      MentorEditResolver,
      ListsResolver,
      MessagesResolver,
      PreventUnsavedChanges
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
