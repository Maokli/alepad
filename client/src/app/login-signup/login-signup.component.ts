import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserAuth } from '../models/user-auth.model';
import { FormBuilder } from '@angular/forms';
import { AccountService} from '../services/account.service';

@Component({
  selector: 'app-login-signup',
  templateUrl: './login-signup.component.html',
  styleUrls: ['./login-signup.component.scss']
})
export class LoginSignupComponent {
  isLogin = true;
  passwordMode = "password";
  eyeState = "open";
  authForm = this.formBuilder.group({
    userName: '',
    password: ''
  });

  constructor(
    private route: ActivatedRoute, 
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router) {
    const path = route.routeConfig.path;
    if(path == 'signup') this.isLogin = false;
   }


  changePasswordState(){
    if(this.eyeState === "open"){
      this.eyeState = "closed";
      this.passwordMode = "text"
    }
    else{
      this.eyeState = "open";
      this.passwordMode = "password"
    }
  }

  loginUser(){
    this.accountService.login(this.authForm.value).subscribe(response => 
      this.router.navigate(['chatrooms']));
  }

  signupUser(){
    this.accountService.signup(this.authForm.value).subscribe(response => 
      this.router.navigate(['chatrooms']));
  }
}
