import { Component, OnInit, Input } from '@angular/core';
import { UserAccount } from 'src/app/_models/userAccount';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-account-dashboard',
  templateUrl: './account-dashboard.component.html',
  styleUrls: ['./account-dashboard.component.css']
})
export class AccountDashboardComponent implements OnInit {
  @Input() account: UserAccount
  role: string;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.role = this.authService.role();
  }

}
