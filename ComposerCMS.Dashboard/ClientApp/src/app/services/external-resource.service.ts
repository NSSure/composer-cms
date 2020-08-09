import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { ExternalResource } from '../../models/ExternalResource';

@Injectable()
export class ExternalResourceService {
  api = 'http://localhost:51494/api/externalresource/';

  constructor(private _http: HttpClient) { }

  list(): Observable<ExternalResource[]> {
    return this._http.get<ExternalResource[]>(this.api.concat('list'))
  }
}
