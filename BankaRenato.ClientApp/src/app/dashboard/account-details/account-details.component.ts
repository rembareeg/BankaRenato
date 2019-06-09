import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/_services/dashboard.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { UserAccount } from 'src/app/_models/userAccount';
import { Card } from 'src/app/_models/card';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit {
  userAccount: UserAccount;
  model: any = {};
  cardTypes: Card [];

  constructor(private dashboardService: DashboardService, private alertify: AlertifyService,
     private route: ActivatedRoute) { }

  ngOnInit() {
    this.model.CardType = 0;
    this.displayAccount();
    this.getCardTypes();
    console.log(this.cardTypes);
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
      console.log(cards);
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

}
