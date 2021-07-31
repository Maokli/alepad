import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Message } from '../models/message.model';
import {take} from 'rxjs/operators';
import { CreateMessageModel } from '../models/create-message.model';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();
  constructor() {
    
   }

   getAccessToken(){
    const userStr = localStorage.getItem("user");
    const token = JSON.parse(userStr).token;

    return token;
   }

  createHubConnection(roomId){
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl+'/chat?roomId='+roomId, {accessTokenFactory: () => this.getAccessToken()})
      .withAutomaticReconnect()
      .build();
    this.hubConnection.start().catch(error => console.log(error));

    this.hubConnection.on("RecievedMessages", messages => {
      this.messageThreadSource.next(messages);
    })

    this.hubConnection.on("NewMessage", (message: Message) => {
      this.messageThread$.pipe(take(1)).subscribe((messages: Message[]) => {
        this.messageThreadSource.next([...messages, message]);
      })
    });
  }

  stopHubConnection() {
    if(this.hubConnection)
      this.hubConnection.stop();
  }

  async sendMessage(message: CreateMessageModel) {
    return this.hubConnection.invoke("SendMessage", message)
      .catch(error => console.log(error));
  }
}
