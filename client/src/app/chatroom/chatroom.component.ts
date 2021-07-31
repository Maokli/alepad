import { Message } from '../models/message.model';
import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { UserLogin } from '../models/user-login.model';
import {ChatService} from '../services/chat.service';
import { AccountService } from '../services/account.service';

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
    private accountService: AccountService,
    public chatService: ChatService) {
    this.user = this.accountService.getCurrentUser();

    this.route.params.pipe(take(1)).subscribe(p => {
      this.chatroom = {id: p.id, name: p.name}
    })

    this.defaultForm = {
      Content: '',
      ChatRoomId: parseInt(this.chatroom.id),
    }
    this.messageForm  = this.formBuilder.group(this.defaultForm);

    this.chatService.createHubConnection(this.chatroom.id);

    this.ScrollToBottomOnNewMessage();
   }

  ngOnDestroy(): void {
    this.chatService.stopHubConnection();
  }

  sendMessage(){
    this.chatService.sendMessage(this.messageForm.value).then(()=>{})
    this.messageForm.reset(this.defaultForm);
  }

  ScrollToBottomOnNewMessage(){
    const targetNode = document.body;
    const config = { childList: true, subtree: true };

    const callback = function(mutationsList, observer) {
        for(let mutation of mutationsList) {
            if (mutation.type === 'childList') {
              const container = document.querySelector(".messages-container");
              container.scrollTop = container.scrollHeight - container.clientHeight;
            }
        }
    };
    const observer = new MutationObserver(callback);
    observer.observe(targetNode, config);
  }
}
