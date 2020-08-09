import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Page } from '../../models/Page';
import { Observable } from 'rxjs';

import Settings from '../../models/Settings';
import { DashboardStats } from '../../models/DashboardStats';
import { Theme } from '../../models/Theme';

@Injectable()
export class SettingsService {
  api = 'http://localhost:51494/api/settings/';
  page: Page;

  constructor(private _http: HttpClient) { }

  update(settings: Settings) {
    return this._http.post<boolean>(this.api.concat("update"), settings);
  }

  getTheme() {
    return this._http.get<string>(this.api.concat("get/theme"));
  }

  setTheme(themeKey: string) {
    return this._http.post<boolean>(this.api.concat("set/theme"), { value: themeKey });
  }

  get() {
    return this._http.get<Settings>(this.api.concat("get"));
  }

  dashboardStats() {
    return this._http.get<DashboardStats>(this.api.concat("dashboard/stats"));
  }

  listThemes() {
    return this._http.get<Theme[]>(this.api.concat("list/themes"));
  }

  syncFileSystem(): Observable<boolean> {
    return this._http.get<boolean>(this.api.concat('file/system/sync'));
  }

  purgeSystem(): Observable<boolean> {
    return this._http.get<boolean>(this.api.concat('system/purge'));
  }
}
