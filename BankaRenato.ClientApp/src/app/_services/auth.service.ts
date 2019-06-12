import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

jwtHelper = new JwtHelperService();
decodedToken: any;

constructor(private http: HttpClient) {}

login(model: any){
  return this.http.post(environment.baseUrl + 'auth/login', model).pipe(
    map((response: any) => {
      const user = response;
      if(user){
        localStorage.setItem('token', user.token);
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        console.log(this.decodedToken);
      }
    }))  
}

register(model: any){
  return this.http.post(environment.baseUrl + 'auth/register', model);
}

loggedIn(){
  return !this.jwtHelper.isTokenExpired(localStorage.getItem('token'));
}

isAdmin() : boolean {
  return this.decodedToken.role == 'Admin';
}

resetToken(){
  localStorage.removeItem('token');
  this.decodedToken = {};
}

}