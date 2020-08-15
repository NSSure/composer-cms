import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import MenuItem from '../../models/MenuItem';

@Injectable()
export class MenuItemService {
  api = 'http://localhost:51494/api/menu/item/';

  constructor(private _http: HttpClient) { }

  add(menuItem: MenuItem): Observable<MenuItem[]> {
    return this._http.post<MenuItem[]>(this.api.concat('add'), menuItem);
  }

  update(menuItem: MenuItem): Observable<MenuItem[]> {
    return this._http.post<MenuItem[]>(this.api.concat('update'), menuItem);
  }

  list(menuId: string): Observable<MenuItem[]> {
    return this._http.post<MenuItem[]>(this.api.concat('list'), { value: menuId });
  }

  incrementOrder(menuId: string, menuItemId: string): Observable<boolean> {
    return this._http.get<boolean>(this.api.concat(`increment/order/${menuId}/${menuItemId}`));
  }

  decrementOrder(menuId: string, menuItemId: string): Observable<boolean> {
    return this._http.get<boolean>(this.api.concat(`decrement/order/${menuId}/${menuItemId}`));
  }
}
