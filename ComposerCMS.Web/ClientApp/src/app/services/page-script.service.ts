import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { PageScript } from '../models/PageScript';
import { ExternalResource } from '../models/ExternalResource';

@Injectable()
export class PageScriptService extends BaseService {
  get api() {
    return `${super.api}/api/page/script/`;
  }

  add(pageScript: PageScript): Observable<PageScript> {
    return this.http.post<PageScript>(this.api.concat('add'), pageScript)
  }

  listScriptResourcesByPage(pageId: string): Observable<Array<ExternalResource>> {
    return this.http.post<Array<ExternalResource>>(this.api.concat('list/resources'), { value: pageId });
  }
}
