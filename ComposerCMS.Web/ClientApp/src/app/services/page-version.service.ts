import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { PageVersion } from '../../models/PageVersion';

@Injectable()
export class PageVersionService {
  api = 'http://localhost:51494/api/page/version/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private _http: HttpClient) { }

  getById(pageVersionId: string): Observable<PageVersion> {
    return this._http.post<PageVersion>(this.api.concat('get/id'), { value: pageVersionId }, this.httpOptions)
  }

  list(pageId: string): Observable<PageVersion[]> {
    return this._http.post<PageVersion[]>(this.api.concat('list'), { value: pageId }, this.httpOptions)
  }
}
