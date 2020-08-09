import { CanActivate, Router } from "@angular/router";
import { Injectable } from "@angular/core";
import { AccountService } from "../services/account.service";

@Injectable()
export class AuthenticatedGuard implements CanActivate {
  constructor(private _accountService: AccountService, private _router: Router) { }

  canActivate(): boolean {
    if (this._accountService.isTokenExpired) {
      this._router.navigate(['login']);
      return false;
    }

    return true;
  }
}
