import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MentorsComponent } from './mentors/mentors.component';
import { MessagesComponent } from './messages/messages.component';
import { LikesComponent } from './likes/likes.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {path: 'mentors', component: MentorsComponent, canActivate: [AuthGuard]},
    {path: 'messages', component: MessagesComponent},
    {path: 'likes', component: LikesComponent},
    {path: '**', redirectTo: 'home', pathMatch: 'full'}
];
