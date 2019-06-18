import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/_services/dashboard.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  model: any = {};

  constructor(private dashboardService: DashboardService, private authService: AuthService, private alertify: AlertifyService,
    private route: ActivatedRoute, private router: Router) { }
  isAdmin: boolean;

  ngOnInit() {
    this.isAdmin = this.authService.isAdmin();

    if(!this.isAdmin)
    {
      if(this.route.snapshot.params['id'] == this.authService.decodedToken.nameid)
      {
        this.loadUserById();
      }else{
        this.alertify.error("Permission denied!");
        this.router.navigate(['dashboard']);
      }
    }else{
      this.loadUserById();
    }
    
  }

  loadUserById(){
    this.dashboardService.getUserById(this.route.snapshot.params['id']).subscribe((user: User) => {
      this.model.id = user.id;
      this.model.Email = user.email;
      this.model.Username = user.username;
      this.model.FirstName = user.firstName;
      this.model.LastName = user.lastName;
      this.model.Password = '';
    }, error => {
      this.alertify.error(error);
    });
  }

  updateAdmin(){
    this.dashboardService.updateUser(this.model).subscribe((user: User) => {
      this.model.id = user.id;
      this.model.Email = user.email;
      this.model.Username = user.username;
      this.model.FirstName = user.firstName;
      this.model.LastName = user.lastName;
      this.model.Password = '';
      this.alertify.success("Successfully changed data");
    }, error => {
      this.alertify.error(error);
    });
  }
  updateUser(){
    this.dashboardService.updateUser(this.model).subscribe((user: User) => {
      this.model.id = user.id;
      this.model.Email = user.email;
      this.model.Username = user.username;
      this.model.FirstName = user.firstName;
      this.model.LastName = user.lastName;
      this.model.Password = '';
      this.alertify.success("Successfully changed data");
    }, error => {
      this.alertify.error(error);
    });
  }

}
