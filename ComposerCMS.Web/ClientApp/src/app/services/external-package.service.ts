import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { ExternalPackage } from '../../models/ExternalPackage';
import { PackageBundle } from '../../models/PackageBundle';

@Injectable()
export class ExternalPackageService {
  api = 'http://localhost:51494/api/external/package/';

  constructor(private _http: HttpClient) { }

  list(): Observable<PackageBundle[]> {
    return this._http.get<PackageBundle[]>(this.api.concat('list'))
  }
}
