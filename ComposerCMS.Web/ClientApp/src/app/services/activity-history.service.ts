import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ActivityHistory } from '../models/ActivityHistory';

@Injectable()
export class ActivityHistoryService extends BaseService {
  get api() {
    return `${super.api}/api/activity/history/`;
  }

  list(): Observable<ActivityHistory[]> {
    return this.http.get<ActivityHistory[]>(this.api.concat('list'))
  }
}
