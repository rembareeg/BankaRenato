import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/_services/dashboard.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserAccount } from 'src/app/_models/userAccount';
import { Card } from 'src/app/_models/card';
import { AuthService } from 'src/app/_services/auth.service';
import { Location} from '@angular/common'

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit {
  userAccount: UserAccount;
  model: any = {};
  updateModel: any = {};
  cardTypes: Card [];
  isAdmin: boolean;
  owner: any = {};
  toggleEdit: number = -1;

  constructor(private dashboardService: DashboardService, private alertify: AlertifyService, 
    private authService: AuthService, private route: ActivatedRoute, private location: Location,
    private router: Router) { }

  ngOnInit() {
    this.model.CardType = 0;
    this.isAdmin = this.authService.isAdmin();
    this.owner.UserId =  this.authService.decodedToken.nameid;
    this.owner.AccountId = this.route.snapshot.params['id'];
    
    if(!this.isAdmin)
    {
      this.dashboardService.userOwnsAccount(this.owner).subscribe(next => {
        this.displayAccount();
        this.getCardTypes();
        },error => {
          this.alertify.error("Permission denied!");
          this.router.navigate(['dashboard']);
        }
      );
    }else{
      this.displayAccount();
      this.getCardTypes();
    }
    
  }
    
  

  displayAccount(){
    this.dashboardService.getAccount(this.route.snapshot.params['id']).subscribe((account: UserAccount) => {
      this.userAccount = account;

    }, error => {
      this.alertify.error(error);
    });
  }

  getCardTypes(){
    this.dashboardService.getCardTypes().subscribe((cards: Card []) => {
      this.cardTypes = cards;
      }, error => {
      this.alertify.error(error);
    });
  }

  createCard(){
    this.model.AccountId = this.userAccount.id;

    this.dashboardService.createCard(this.model).subscribe(next => {
      this.alertify.success('Success');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.displayAccount();
    });
  }

  deleteCard(id: number){
    this.dashboardService.deleteCard(id).subscribe(
      () => {
       this.alertify.success("Card with id: " + id + " successfully deleted");
       this.displayAccount();
      },error =>{
        this.alertify.error(error) 
      }
    );
  }
  editCard(id: number, cardType: number){
    this.toggleEdit = id;
    this.updateModel.CardType = cardType;
    this.displayAccount();
  }

  updateCard(){
    this.updateModel.Id = this.toggleEdit;
    this.dashboardService.updateCard(this.updateModel).subscribe(next => {
      this.alertify.success('Success');
    }, error => {
      this.alertify.error(error);
    }, () => {
      
      this.displayAccount();
      this.toggleEdit = -1;
    });
  }

  back(){
    this.location.back();
  }
}
