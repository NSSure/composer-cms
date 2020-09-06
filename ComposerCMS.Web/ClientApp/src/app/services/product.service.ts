import { Injectable } from '@angular/core';

// Import RxJs required methods
import { BaseService } from './base.service';

@Injectable()
export class ProductService extends BaseService {
  get api() {
    return `${super.api}/api/product/`;
  }
}
