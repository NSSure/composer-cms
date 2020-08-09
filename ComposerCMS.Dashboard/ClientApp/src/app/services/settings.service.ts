import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Page } from '../../models/Page';
import { Observable } from 'rxjs';
import Settings from '../../models/Settings';

@Injectable()
export class SettingsService {
  api = 'http://localhost:51494/api/settings/';
  page: Page;

  constructor(private _http: HttpClient) { }

  update(settings: Settings) {
    return this._http.post<boolean>(this.api.concat("update"), settings);
  }

  get() {
    return this._http.get<Settings>(this.api.concat("get"));
  }

  purgeSystem(): Observable<boolean> {
    return this._http.get<boolean>(this.api.concat('system/purge'));
  }
}
