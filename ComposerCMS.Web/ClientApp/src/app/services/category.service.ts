import { Injectable } from '@angular/core';

// Import RxJs required methods
import { BaseService } from './base.service';

import Category from '../models/Category';
import { Observable } from 'rxjs';

@Injectable()
export class CategoryService extends BaseService {
  get api() {
    return `${super.api}/api/category`;
  }

  add(category: Category) {
    return this.http.post(this.api.concat('add'), category);
  }

  update(category: Category) {
    return this.http.post(this.api.concat('update'), category);
  }

  list(): Observable<Category[]> {
    return this.http.get<Category[]>(this.api.concat('list'))
  }
}
