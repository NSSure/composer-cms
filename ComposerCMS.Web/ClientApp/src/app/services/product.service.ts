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

  add(product: any): Observable<any> {
    return this.http.post<any>(this.api.concat('add'), product)
  }

  get(productId: string): Observable<Product> {
    return this.http.get<Product>(this.api.concat('get/' + productId));
  }

  list(): Observable<Product[]> {
    return this.http.get<Product[]>(this.api.concat('list'))
  }
}
