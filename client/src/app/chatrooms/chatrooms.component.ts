import { Component, OnInit } from '@angular/core';
import { ChatRoom } from '../models/chatroom.model';
import { ChatroomService } from '../services/chatroom.service';

@Component({
  selector: 'app-chatrooms',
  templateUrl: './chatrooms.component.html',
  styleUrls: ['./chatrooms.component.scss']
})
export class ChatroomsComponent{
  chatRooms: ChatRoom[];

  constructor(private chatroomService: ChatroomService) {
    this.chatroomService.getAllChatRooms()
      .subscribe(response => this.chatRooms = response);
   }


}
