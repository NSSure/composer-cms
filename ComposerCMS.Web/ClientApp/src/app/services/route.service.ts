import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import Route from '../models/Route';

@Injectable()
export class RouteService extends BaseService {
  get api() { 
    return `${super.api}/api/route/`;
  }

  list(): Observable<Route[]> {
    return this.http.get<Route[]>(this.api.concat('list'), this.httpOptions)
  }
}
