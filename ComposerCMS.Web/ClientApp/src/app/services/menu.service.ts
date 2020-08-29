import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { Menu } from '../models/Menu';

@Injectable()
export class MenuService extends BaseService {
  get api() {
    return `${super.api}/api/menu/`;
  }

  add(menu: Menu): Observable<Menu[]> {
    return this.http.post<Menu[]>(this.api.concat('add'), menu);
  }

  update(menu: Menu): Observable<Menu[]> {
    return this.http.post<Menu[]>(this.api.concat('update'), menu);
  }

  get(menuId: string) {
    return this.http.get<Menu>(this.api.concat(`get/${menuId}`));
  }

  list(): Observable<Menu[]> {
    return this.http.get<Menu[]>(this.api.concat('list'));
  }
}
