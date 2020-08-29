import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { Page } from '../models/Page';

@Injectable()
export class PageService extends BaseService {
  get api() {
    return `${super.api}/api/page/`;
  }

  page: Page;

  add(page: Page): Observable<Page> {
    return this.http.post<Page>(this.api.concat('add'), page)
  }

  update(page: Page): Observable<Page> {
    return this.http.post<Page>(this.api.concat('update'), page)
  }

  publish(page: Page): Observable<Page> {
    return this.http.post<Page>(this.api.concat('publish'), page)
  }

  list(): Observable<Page[]> {
    return this.http.get<Page[]>(this.api.concat('list'), this.httpOptions)
  }

  getByID(id: string): Observable<Page> {
    return this.http.post<Page>(this.api.concat('get/id'), { Value: id });
  }
}
