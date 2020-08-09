import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { Menu } from '../../models/Menu';

@Injectable()
export class MenuService {
  api = 'http://localhost:51494/api/menu/';

  constructor(private _http: HttpClient) { }

  add(menu: Menu): Observable<Menu[]> {
    return this._http.post<Menu[]>(this.api.concat('add'), menu);
  }

  update(menu: Menu): Observable<Menu[]> {
    return this._http.post<Menu[]>(this.api.concat('update'), menu);
  }

  get(menuId: string) {
    return this._http.get<Menu>(this.api.concat(`get/${menuId}`));
  }

  list(): Observable<Menu[]> {
    return this._http.get<Menu[]>(this.api.concat('list'));
  }
}
