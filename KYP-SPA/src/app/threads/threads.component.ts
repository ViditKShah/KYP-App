import { Component, OnInit, Input } from '@angular/core';
import { Message } from '../_models/message';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-threads',
  templateUrl: './threads.component.html',
  styleUrls: ['./threads.component.css']
})
export class ThreadsComponent implements OnInit {
  @Input() recipientId: number;
  messages: Message[];

  constructor(private userService: UserService, private authService: AuthService,
    private alertifyService: AlertifyService) { }

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages() {
    this.userService
        .getMessageThread(this.authService.decodedToken.nameid, this.recipientId)
        .subscribe((messages: Message[]) => {
          this.messages = messages;
        }, (error: any) => {
          this.alertifyService.error(error);
        });
  }

}
