import { Injectable } from '@angular/core';

// Sample data import.
import { BehaviorSubject } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { UserRegistration } from '../../models/UserRegistration';
import { UserSignIn } from '../../models/UserSignIn';
import { Router } from '@angular/router';

@Injectable()
export class AccountService {
  api = 'http://localhost:51494/api/account/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private _http: HttpClient, private _router: Router) { }

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

  register(userRegistration: UserRegistration) {
    return this._http.post(this.api + 'register', userRegistration, this.httpOptions);
  }

  logout() {
    this._http.get<string>(this.api + 'logout', this.httpOptions).subscribe((rtn) => {
      this.setIsAuthenticated(false);
      localStorage.removeItem('userToken');
      this._router.navigate(['login']);
    });
  }

  login(userSignIn: UserSignIn) {
    this._http.post<string>(this.api + 'login', userSignIn, this.httpOptions).subscribe(token => {
      this.setIsAuthenticated(true);
      localStorage.setItem('userToken', JSON.stringify(token));
    });
  }
}
