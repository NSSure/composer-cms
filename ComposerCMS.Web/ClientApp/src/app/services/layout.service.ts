import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { Layout } from '../models/Layout';

@Injectable()
export class LayoutService extends BaseService {
  get api() {
    return `${super.api}/api/layout/`;
  }

  add(layout: Layout): Observable<Layout> {
    return this.http.post<Layout>(this.api.concat('add'), layout)
  }

  list(): Observable<Layout[]> {
    return this.http.get<Layout[]>(this.api.concat('list'))
  }
}
