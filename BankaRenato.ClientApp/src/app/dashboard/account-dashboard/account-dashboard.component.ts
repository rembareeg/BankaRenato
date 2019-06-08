import { Component, OnInit, Input } from '@angular/core';
import { UserAccount } from 'src/app/_models/userAccount';

@Component({
  selector: 'app-account-dashboard',
  templateUrl: './account-dashboard.component.html',
  styleUrls: ['./account-dashboard.component.css']
})
export class AccountDashboardComponent implements OnInit {
  @Input() account: UserAccount

  constructor() { }

  ngOnInit() {
    
  }

}
