import { Message } from '../models/message.model';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { UserLogin } from '../models/user-login.model';
import {ChatService} from '../services/chat.service';

@Component({
  selector: 'app-chatroom',
  templateUrl: './chatroom.component.html',
  styleUrls: ['./chatroom.component.scss']
})
export class ChatroomComponent implements OnDestroy {
  user: UserLogin;
  messageForm;
  chatroom;
  private defaultForm;

  constructor(
    private formBuilder: FormBuilder, 
    private route: ActivatedRoute,
    public chatService: ChatService) {
    const userString = localStorage.getItem("user");

    this.route.params.pipe(take(1)).subscribe(p => {
      this.chatroom = {id: p.id, name: p.name}
    })

    this.user = JSON.parse(userString);
    this.defaultForm = {
      SenderId: this.user.id,
      SenderUsername: this.user.userName,
      Content: '',
      ChatRoomId: parseInt(this.chatroom.id),
    }
    this.messageForm  = this.formBuilder.group(this.defaultForm);

    this.chatService.createHubConnection(this.chatroom.id);
   }

  ngOnDestroy(): void {
    this.chatService.stopHubConnection();
  }

  sendMessage(){
    this.chatService.sendMessage(this.messageForm.value).then(()=>{})
    this.messageForm.reset(this.defaultForm);
  }
}
