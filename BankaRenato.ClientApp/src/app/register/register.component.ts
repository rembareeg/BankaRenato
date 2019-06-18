import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    if(this.authService.loggedIn() && !this.authService.isAdmin)
    {
      this.router.navigate(['home']);
    }
  }

  register() {
    this.authService.register(this.model).subscribe(next => {
      this.alertify.success('registration successfull');
      this.router.navigate(['home']);
    }, error => {
      this.alertify.error(error);
    })
  }
}
