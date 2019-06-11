import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AuthService } from './auth.service';
import { UserAccount } from '../_models/userAccount';
import { Card } from '../_models/card';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {


  constructor(private http: HttpClient, private authService: AuthService) { }

  getUser(): Observable<User>{
    return this.http.get<User>(environment.baseUrl + 'dashboard/getuser/' + this.authService.decodedToken.nameid);
  }

  getUserById(id: number): Observable<User>{

    return this.http.get<User>(environment.baseUrl + 'dashboard/getuser/' + id);
  }

  getUsersByRole(role: string): Observable<User[]>{
    return this.http.get<User[]>(environment.baseUrl + 'dashboard/getusersbyrole/' + role);
  }

  getAccount(id: number): Observable<UserAccount>{
    return this.http.get<UserAccount>(environment.baseUrl + 'dashboard/getaccount/' + id);
  }
  
  openAccount(user: User){    
    return this.http.post(environment.baseUrl + 'dashboard/openaccount', user);   
  }

  getCardTypes(): Observable<Card []>{   
    return this.http.get<Card []>(environment.baseUrl + 'dashboard/getcardtypes');   
  }

  createCard(model: any){
    return this.http.post(environment.baseUrl + 'dashboard/createcard', model);
  }

  deleteUser(id: number){
    return this.http.delete(environment.baseUrl + 'dashboard/deleteuser/' + id);
  }

  deleteAccount(id: number){
    return this.http.delete(environment.baseUrl + 'dashboard/deleteaccount/' + id);
  }

  deleteCard(id: number){
    return this.http.delete(environment.baseUrl + 'dashboard/deletecard/' + id);
  }
  updateCard(card: Card){
    return this.http.post(environment.baseUrl + 'dashboard/updatecard/', card);
  }
  updateUser(model: any){
    return this.http.put(environment.baseUrl + 'dashboard/updateuser/', model);
  }
}
