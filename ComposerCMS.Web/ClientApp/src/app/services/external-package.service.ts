import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { PackageBundle } from '../models/PackageBundle';
import { BaseService } from './base.service';

@Injectable()
export class ExternalPackageService extends BaseService {
  get api() {
    return `${super.api}/api/external/package/`;
  }

  list(): Observable<PackageBundle[]> {
    return this.http.get<PackageBundle[]>(this.api.concat('list'))
  }
}
