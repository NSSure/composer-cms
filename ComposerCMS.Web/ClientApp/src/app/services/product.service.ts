import { Injectable } from '@angular/core';

// Import RxJs required methods
import { BaseService } from './base.service';
import Product from '../models/Product';
import { Observable } from 'rxjs';

@Injectable()
export class ProductService extends BaseService {
  get api() {
    return `${super.api}/api/productsystem/product/`;
  }

  list(): Observable<Product[]> {
    return this.http.get<Product[]>(this.api.concat('list'))
  }
}
