import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, of, ReplaySubject } from 'rxjs';
import { ChatRoom } from '../models/chatroom.model';
import { AccountService } from './account.service';
import { map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ChatroomService {
  baseApiUrl = environment.apiUrl;
  token: string;
  chatroomsCache = new Map();
  chatrooms: ChatRoom[] = [];

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.token = 'Bearer '+this.accountService.getCurrentUser().token;
  }

  getAllChatRooms(): Observable<ChatRoom[]>{
    const chachedResults = this.chatroomsCache.get("chatrooms");

    if(chachedResults) return of(chachedResults);


    return this.http.get<ChatRoom[]>(this.baseApiUrl + '/chatroom', {
      headers: new HttpHeaders()
        .set('Authorization', this.token)
        .set('Content-Type', 'application/json')
    }).pipe(map(response => {
      this.chatroomsCache.set("chatrooms",response);
      return response;
    }))
  }

  
}
