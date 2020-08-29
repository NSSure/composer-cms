import { Injectable } from '@angular/core';

// Sample data import.
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { environment } from '../../environments/environment';

@Injectable()
export class BaseService {
  get api() {
    return environment.api;
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(public http: HttpClient, public router: Router) {
    console.log(environment.api);
  }
}
