import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { ActivityHistory } from '../../models/ActivityHistory';

@Injectable()
export class ActivityHistoryService {
  api = 'http://localhost:51494/api/activity/history/';

  constructor(private _http: HttpClient) { }

  list(): Observable<ActivityHistory[]> {
    return this._http.get<ActivityHistory[]>(this.api.concat('list'))
  }
}
