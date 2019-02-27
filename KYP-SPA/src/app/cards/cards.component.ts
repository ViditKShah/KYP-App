import { Component, Input, OnInit } from '@angular/core';
import { User } from '../_models/user';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {
  @Input() user: User;

  constructor() { }

  ngOnInit() {
  }

}
