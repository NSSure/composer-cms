import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

// Import RxJs required methods
import { Page } from '../../models/Page';
import { Observable } from 'rxjs';
import Route from '../../models/Route';

@Injectable()
export class RouteService {
  api = 'http://localhost:51494/api/route/';
  page: Page;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private _http: HttpClient) { }

  list(): Observable<Route[]> {
    return this._http.get<Route[]>(this.api.concat('list'), this.httpOptions)
  }
}
