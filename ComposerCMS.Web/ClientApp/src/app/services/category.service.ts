import { Injectable } from '@angular/core';

// Import RxJs required methods
import { BaseService } from './base.service';

import Category from '../models/Category';
import { Observable } from 'rxjs';

@Injectable()
export class CategoryService extends BaseService {
  get api() {
    return `${super.api}/api/productsystem/category/`;
  }

  add(category: Category) {
    return this.http.post(this.api.concat('add'), category);
  }

  update(category: Category) {
    return this.http.post(this.api.concat('update'), category);
  }

  get(categoryId: string) {
    return this.http.get<Category>(this.api.concat(`get/${categoryId}`));
  }

  list(): Observable<Category[]> {
    return this.http.get<Category[]>(this.api.concat('list'))
  }
}
