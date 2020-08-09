import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

// Import RxJs required methods
import { Page } from '../../models/Page';
import { Observable } from 'rxjs';

@Injectable()
export class PageService {
  api = 'http://localhost:51494/api/page/';
  page: Page;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private _http: HttpClient) { }

  add(page: Page): Observable<Page> {
    return this._http.post<Page>(this.api.concat('add'), page)
  }

  update(page: Page): Observable<Page> {
    return this._http.post<Page>(this.api.concat('update'), page)
  }

  publish(page: Page): Observable<Page> {
    return this._http.post<Page>(this.api.concat('publish'), page)
  }

  list(): Observable<Page[]> {
    return this._http.get<Page[]>(this.api.concat('list'), this.httpOptions)
  }

  getByID(id: string): Observable<Page> {
    return this._http.post<Page>(this.api.concat('get/id'), { Value: id });
  }
}
