import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { DashboardService } from '../_services/dashboard.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  user: User;
  role: string;

  constructor(private dashboardService: DashboardService, private authService: AuthService, private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  

  ngOnInit() {
    this.getUser();
    this.role = this.authService.role();
    
  }

  openAccount(){
    this.alertify.authorize("Account open", "Are you sure you want to open new account", () => {
      this.dashboardService.openAccount(this.user).subscribe(next => {
        this.alertify.success("Successfully created new account");
      },error => {
        this.alertify.error(error);
      }, () =>{
        this.getUser();
      })
    });  
  }  

  loadUser() {
    this.dashboardService.getUser().subscribe((user: User) => {
      this.user = user;
    }, error => {
      this.alertify.error(error);
    });
  }

  loadUserById(){
    this.dashboardService.getUserById(this.route.snapshot.params['id']).subscribe((user: User) => {
      this.user = user;
    }, error => {
      this.alertify.error(error);
    });
  }
  
  getUser(){
    console.log(0);
    if(this.route.snapshot.params['id'] == null){
      this.loadUser();
      console.log(1);
    }else{
      this.loadUserById();
      console.log(2);
    }
  }
  deleteAccount(id: number){
    this.dashboardService.deleteAccount(id).subscribe(
      () => {
        this.getUser();
       this.alertify.success("Account with id: " + id + " successfully deleted");
       
      },error =>{
        this.alertify.error(error) 
      }
    );
    
  }
  

}
