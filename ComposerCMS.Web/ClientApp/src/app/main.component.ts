import { Component } from '@angular/core';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html'
})
export class MainComponent {
  get currentUserLetter() {
    return this._accountService.currentUsername.charAt(0);
  }

  get currentUsername() {
    return this._accountService.currentUsername;
  }

  get currentRole() {
    return this._accountService.currentRole;
  }

  constructor(private _accountService: AccountService) {

  }

  logout() {
    this._accountService.logout();
  }
}
