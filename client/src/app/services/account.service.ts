import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { UserAuth } from '../models/user-auth.model';
import { UserLogin } from '../models/user-login.model';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from  'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new ReplaySubject<UserLogin>(1);
  user$ = this.currentUserSource.asObservable();
  baseApiUrl = environment.apiUrl;


  constructor(private http: HttpClient) { }

  login(user: UserAuth){
    return this.http.post<UserLogin>(this.baseApiUrl+"/auth/login", user).pipe(
      map((response :UserLogin) => {
        const user = response;
        if(user) this.setCurrentUser(user);

        return response;
      })
    );
  }

  signup(user: UserAuth){
    return this.http.post<UserLogin>(this.baseApiUrl+"/auth/signup", user).pipe(
      map((response :UserLogin) => {
        const user = response;
        if(user) this.setCurrentUser(user);
      })
    );
  }

  setCurrentUser(user: UserLogin){
    localStorage.setItem('user',JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
