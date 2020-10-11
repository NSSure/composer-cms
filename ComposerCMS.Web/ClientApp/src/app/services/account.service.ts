import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { UserRegistration } from '../models/UserRegistration';
import { UserSignIn } from '../models/UserSignIn';

@Injectable()
export class AccountService extends BaseService {
  get api() {
    return `${super.api}/api/account/`;
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  private _isAuthenticatedSource = new BehaviorSubject<boolean>(!this.isTokenExpired);

  isAuthenticated = this._isAuthenticatedSource.asObservable();

  setIsAuthenticated(isAuthenticated) {
    this._isAuthenticatedSource.next(isAuthenticated);
  }

  get token() {
    let userToken = JSON.parse(localStorage.getItem('userToken'));

    if (userToken) {
      // [0] = Header, [1] = Payload, [2] = Signature
      let tokenComponents = userToken.token.split('.');
      let payload = tokenComponents[1];

      let payloadJson = atob(payload);

      return JSON.parse(payloadJson);
    }

    return null;
  }

  get tokenExpiration(): Date {
    if (this.token) {
      return new Date(this.token.exp * 1000);
    }

    return null;
  }

  get isTokenExpired(): boolean {
    if (this.tokenExpiration) {
      return this.tokenExpiration < new Date();
    }

    return true;
  }

  get currentUsername() {
    if (this.token) {
      return this.token.name;
    }
  }

  get currentRole() {
    if (this.token) {
      return this.token.role;
    }
  }

  register(userRegistration: UserRegistration) {
    return this.http.post(this.api + 'register', userRegistration, this.httpOptions);
  }

  logout() {
    this.http.get<string>(this.api + 'logout', this.httpOptions).subscribe((rtn) => {
      this.setIsAuthenticated(false);
      localStorage.removeItem('userToken');
      this.router.navigate(['login']);
    });
  }

  login(userSignIn: UserSignIn) {
    this.http.post<string>(this.api + 'login', userSignIn, this.httpOptions).subscribe(userToken => {
      this.setIsAuthenticated(true);

      localStorage.setItem('userToken', JSON.stringify(userToken));

      console.log(this.currentUsername);
      console.log(this.currentRole);
    });
  }
}
