import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, ReplaySubject } from 'rxjs';
import { ChatRoom } from '../models/chatroom.model';
import { AccountService } from './account.service';
import { map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ChatroomService {
  baseApiUrl = environment.apiUrl;
  token: string;
  private _chatroomsSource = new ReplaySubject<ChatRoom[]>(1);
  chatrooms$ = this._chatroomsSource.asObservable();

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.token = 'Bearer '+this.accountService.getCurrentUser().token;
  }

  getAllChatRooms(): Observable<ChatRoom[]>{
    
      return this.http.get<ChatRoom[]>(this.baseApiUrl + '/chatroom', {
        headers: new HttpHeaders()
          .set('Authorization', this.token)
          .set('Content-Type', 'application/json')
      })
  }

  setChatrooms(chatrooms: ChatRoom[]){
    this._chatroomsSource.next(chatrooms);
  }
  
}
