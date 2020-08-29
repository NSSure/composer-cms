import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { PageVersion } from '../models/PageVersion';

@Injectable()
export class PageVersionService extends BaseService {
  get api() {
    return `${super.api}/api/page/version/`;
  }

  getById(pageVersionId: string): Observable<PageVersion> {
    return this.http.post<PageVersion>(this.api.concat('get/id'), { value: pageVersionId }, this.httpOptions)
  }

  list(pageId: string): Observable<PageVersion[]> {
    return this.http.post<PageVersion[]>(this.api.concat('list'), { value: pageId }, this.httpOptions)
  }
}
