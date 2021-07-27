import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatroomsComponent } from './chatrooms/chatrooms.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginSignupComponent } from './login-signup/login-signup.component';
import {AuthGuardService as AuthGuard} from './guards/auth-guard.service';


const routes: Routes = [
  {
    path: '',
    component: HomePageComponent
  },
  {
    path: 'login',
    component: LoginSignupComponent
  },
  {
    path: 'signup',
    component: LoginSignupComponent
  },
  {
    path:'chatrooms',
    component: ChatroomsComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
