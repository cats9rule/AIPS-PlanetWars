import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.scss']
})
export class AuthorizeComponent implements OnInit {

  public isLogging = true;

  constructor() { }

  ngOnInit(): void {
  }

  public toggleView() {
    this.isLogging = !this.isLogging;
  }

}
