import { Injectable } from '@angular/core';

// Import RxJs required methods
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable()
export class ProductCategoryService extends BaseService {
  get api() {
    return `${super.api}/api/productsystem/productcategory/`;
  }

  assign(productId: string, categoryId: string): Observable<boolean> {
    return this.http.get<boolean>(this.api.concat(`assign/${productId}/${categoryId}`))
  }

  unassign(productId: string, categoryId: string): Observable<boolean> {
    return this.http.get<boolean>(this.api.concat(`unassign/${productId}/${categoryId}`));
  }
}
