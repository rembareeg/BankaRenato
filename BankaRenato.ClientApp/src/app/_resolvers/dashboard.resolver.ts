import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { DashboardService } from '../_services/dashboard.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class DashboardResolver implements Resolve<User>{

    constructor(private dashboardService: DashboardService, private router: Router, 
        private alertify: AlertifyService){}
    
        resolve(route: ActivatedRouteSnapshot): Observable<User>{
            return this.dashboardService.getUserById(route.params['id']).pipe(
                catchError(error =>{
                    this.alertify.error('Problem retriving data');
                    this.router.navigate(['/home']);
                    return of(null);
                })
            );
        }
}