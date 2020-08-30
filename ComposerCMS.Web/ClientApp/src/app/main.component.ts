import { Component } from '@angular/core';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html'
})
export class MainComponent {
  constructor(private _accountService: AccountService) {

  }

  logout() {
    this._accountService.logout();
  }
}
