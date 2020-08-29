import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';

import { BaseService } from './base.service';
import Theme from '../models/Theme';
import DashboardStats from '../models/DashboardStats';
import Settings from '../models/Settings';

@Injectable()
export class SettingsService extends BaseService {
  get api() {
    return `${super.api}/api/settings/`;
  }

  update(settings: Settings) {
    return this.http.post<boolean>(this.api.concat("update"), settings);
  }

  getTheme() {
    return this.http.get<string>(this.api.concat("get/theme"));
  }

  setTheme(themeKey: string) {
    return this.http.post<boolean>(this.api.concat("set/theme"), { value: themeKey });
  }

  get() {
    return this.http.get<Settings>(this.api.concat("get"));
  }

  dashboardStats() {
    return this.http.get<DashboardStats >(this.api.concat("dashboard/stats"));
  }

  listThemes() {
    return this.http.get<Theme[]>(this.api.concat("list/themes"));
  }

  syncFileSystem(): Observable<boolean> {
    return this.http.get<boolean>(this.api.concat('file/system/sync'));
  }

  purgeSystem(): Observable<boolean> {
    return this.http.get<boolean>(this.api.concat('system/purge'));
  }
}
