import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { PageScript } from '../../models/PageScript';
import { Observable } from 'rxjs';
import { ExternalResource } from '../../models/ExternalResource';

@Injectable()
export class PageScriptService {
  api = 'http://localhost:51494/api/page/script/';

  constructor(private _http: HttpClient) { }

  add(pageScript: PageScript): Observable<PageScript> {
    return this._http.post<PageScript>(this.api.concat('add'), pageScript)
  }

  listScriptResourcesByPage(pageId: string): Observable<Array<ExternalResource>> {
    return this._http.post<Array<ExternalResource>>(this.api.concat('list/resources'), { value: pageId });
  }
}
