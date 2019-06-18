import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { DashboardService } from '../_services/dashboard.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  user: User;
  isAdmin: boolean;
  model: any  = {};
  updateModel: any  = {};
  toggleEdit: number = -1;

  constructor(private dashboardService: DashboardService, private authService: AuthService, private alertify: AlertifyService,
    private route: ActivatedRoute, private router: Router) { }

  

  ngOnInit() {
    this.getUser();
    this.isAdmin = this.authService.isAdmin();
    
  }

  openAccount(){
    this.model.UserId = this.user.id;
    this.alertify.authorize("Account open", "Are you sure you want to open new account", () => {
      this.dashboardService.openAccount(this.model).subscribe(next => {
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
    if(this.route.snapshot.params['id'] == null){
      this.loadUser();
    }else{
      this.loadUserById();
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

  editAccount(id: number, name: string, balance: number){
    this.toggleEdit = id;
    this.updateModel.Name = name;
    this.updateModel.Balance = balance;
    this.loadUserById();
  }
  updateAccount(){
    this.updateModel.Id = this.toggleEdit;
    this.dashboardService.updateAccount(this.updateModel).subscribe(next => {
      this.alertify.success('Success');
    }, error => {
      this.alertify.error(error);
    }, () => {
      
      this.loadUserById();
      this.toggleEdit = -1;
    });
  }
  edit(id: number){
    this.router.navigate([ '/edit-user/', id]);
  }

}
