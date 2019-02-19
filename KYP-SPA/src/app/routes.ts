import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MentorsComponent } from './mentors/mentors.component';
import { MessagesComponent } from './messages/messages.component';
import { LikesComponent } from './likes/likes.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'mentors', component: MentorsComponent},
            {path: 'messages', component: MessagesComponent},
            {path: 'likes', component: LikesComponent}
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
