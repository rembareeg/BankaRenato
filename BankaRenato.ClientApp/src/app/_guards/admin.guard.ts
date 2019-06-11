import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService){

  }

  canActivate():  boolean {
    if(this.authService.role() === 'Admin') {
      return true;
    }
    
    this.alertify.error("Permission denied!");
    this.router.navigate(['/home']);
    return false;
  }
  
  
}
