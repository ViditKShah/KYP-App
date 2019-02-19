import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MentorsComponent } from './mentors/mentors.component';
import { MessagesComponent } from './messages/messages.component';
import { LikesComponent } from './likes/likes.component';

export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {path: 'mentors', component: MentorsComponent},
    {path: 'messages', component: MessagesComponent},
    {path: 'likes', component: LikesComponent},
    {path: '**', redirectTo: 'home', pathMatch: 'full'}
];
