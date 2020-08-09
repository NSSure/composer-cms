import { Component } from '@angular/core';
import { UserSignIn } from '../../models/UserSignIn';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userSignIn: UserSignIn = new UserSignIn();

  constructor(private _router: Router, private _accountService: AccountService) {
    this._accountService.isAuthenticated.subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        this._router.navigate(['']);
      }
    })
  }

  login() {
    this._accountService.login(this.userSignIn);
  }
}
