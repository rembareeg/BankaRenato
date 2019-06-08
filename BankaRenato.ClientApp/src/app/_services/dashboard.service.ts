import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  getUser(): Observable<User>{
    return this.http.get<User>(environment.baseUrl + 'dashboard/getuser/' + this.authService.decodedToken.nameid);
  }
  
  openAccount(){
    return this.http.post(environment.baseUrl + 'dashboard/openaccount', this.authService.decodedToken.nameid);
  }
}
