import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  isAdmin: boolean;
  constructor(public authService: AuthService, private  alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    
  }

  login(){
    
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
      this.isAdmin = this.authService.isAdmin();
    }, error => {
      this.alertify.error(error);
    }, () => {
      if(this.isAdmin){
        this.router.navigate(['dashboard-admin']);
      }else{
        this.router.navigate(['dashboard']);
      }
    });
  }

  loggedIn(){
    return this.authService.loggedIn();
  }

  

  logout(){
    this.authService.resetToken();
    this.isAdmin = this.authService.isAdmin();
    this.alertify.message('logged out');
    this.router.navigate(['home']);
  }

}
