import { Component, Input, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {
  @Input() user: User;

  constructor(private authService: AuthService, private userService: UserService,
    private alertifyService: AlertifyService) { }

  ngOnInit() {

  }

  sendLike(recipientId: number) {
    this.userService.sendLike(this.authService.decodedToken.nameid, recipientId)
        .subscribe(data => {
          this.alertifyService.success('You have liked ' + this.user.knownAs);
        }, error => {
          this.alertifyService.error(error);
        });
  }

}
