import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './_guards/auth.guard';
import { AccountDetailsComponent } from './dashboard/account-details/account-details.component';
import { AdminGuard } from './_guards/admin.guard';
import { AdminDashboardComponent } from './dashboard/admin-dashboard/admin-dashboard.component';


const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'register', component: RegisterComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'dashboard', component: DashboardComponent},
      {path: 'dashboard/account/:id', component: AccountDetailsComponent},
      {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AdminGuard],
        children: [
          {path: 'dashboard-admin', component: AdminDashboardComponent},
          {path: 'dashboard/:id', component: DashboardComponent},
         
        ]
      }
    ]
  },  
  {path: '**', redirectTo: 'home', pathMatch: 'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
