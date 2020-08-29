import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ExternalResource } from '../models/ExternalResource';

@Injectable()
export class ExternalResourceService extends BaseService {
  get api() {
    return `${super.api}/api/external/resource/`;
  }

  list(): Observable<ExternalResource[]> {
    return this.http.get<ExternalResource[]>(this.api.concat('list'))
  }
}
