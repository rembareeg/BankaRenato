import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/_services/dashboard.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  constructor(private dashboardService: DashboardService, private alertify: AlertifyService) { }
  users: User[];

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers(){
    this.dashboardService.getUsersByRole('Client').subscribe((users: User[]) => {
      this.users = users;
    }, error => {
      this.alertify.error(error);
    });
  }
  deleteUser(id: number){
    this.dashboardService.deleteUser(id).subscribe(
      () => {
       this.alertify.success("User with id: " + id + " successfully deleted");
       this.loadUsers();
      },error =>{
        this.alertify.success(error) 
      }
      
    );
  }

}
