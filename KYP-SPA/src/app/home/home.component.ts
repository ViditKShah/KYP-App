import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {}

  registerModeOn() {
    this.registerMode = true;
  }

  registerModeOff(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}
