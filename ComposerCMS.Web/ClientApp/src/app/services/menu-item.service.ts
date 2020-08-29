import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import MenuItem from '../models/MenuItem';

@Injectable()
export class MenuItemService extends BaseService {
  get api() {
    return `${super.api}/api/menu/item/`;
  }

  add(menuItem: MenuItem): Observable<MenuItem[]> {
    return this.http.post<MenuItem[]>(this.api.concat('add'), menuItem);
  }

  update(menuItem: MenuItem): Observable<MenuItem[]> {
    return this.http.post<MenuItem[]>(this.api.concat('update'), menuItem);
  }

  list(menuId: string): Observable<MenuItem[]> {
    return this.http.post<MenuItem[]>(this.api.concat('list'), { value: menuId });
  }

  incrementOrder(menuId: string, menuItemId: string): Observable<boolean> {
    return this.http.get<boolean>(this.api.concat(`increment/order/${menuId}/${menuItemId}`));
  }

  decrementOrder(menuId: string, menuItemId: string): Observable<boolean> {
    return this.http.get<boolean>(this.api.concat(`decrement/order/${menuId}/${menuItemId}`));
  }
}
