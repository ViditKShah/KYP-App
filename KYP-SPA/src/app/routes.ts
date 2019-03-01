import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MentorsComponent } from './mentors/mentors.component';
import { MessagesComponent } from './messages/messages.component';
import { LikesComponent } from './likes/likes.component';
import { AuthGuard } from './_guards/auth.guard';
import { DetailsComponent } from './details/details.component';
import { MentorDetailResolver } from './_resolvers/mentor-detail.resolver';
import { MentorListResolver } from './_resolvers/mentor-list.resolver';
import { EditComponent } from './edit/edit.component';
import { MentorEditResolver } from './_resolvers/mentor-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'mentors', component: MentorsComponent,
                resolve: {users: MentorListResolver}},
            {path: 'mentors/:id', component: DetailsComponent,
                resolve: {user: MentorDetailResolver}},
            {path: 'mentor/edit', component: EditComponent,
                resolve: {user: MentorEditResolver},
                canDeactivate: [PreventUnsavedChanges]},
            {path: 'messages', component: MessagesComponent},
            {path: 'likes', component: LikesComponent}
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
