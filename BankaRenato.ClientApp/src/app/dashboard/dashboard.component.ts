import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { DashboardService } from '../_services/dashboard.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  user: User;


  constructor(private dashboardService: DashboardService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadUser();
  }

  openAccount(){
    this.alertify.authorize("Account open", "Are you sure you want to open new account", () => {
      this.dashboardService.openAccount(this.user).subscribe(next => {
        this.alertify.success("Successfully created new account");
      },error => {
        this.alertify.error(error);
      }, () =>{
        this.loadUser();
      })
    });  
  }  

  loadUser() {
    this.dashboardService.getUser().subscribe((user: User) => {
      this.user = user;
      console.log(user.accounts);
    }, error => {
      this.alertify.error(error);
    });
  }

}
