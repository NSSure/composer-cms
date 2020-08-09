import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { Layout } from '../../models/Layout';

@Injectable()
export class LayoutService {
  api = 'http://localhost:51494/api/layout/';

    constructor(private _http: HttpClient) { }

    add(layout: Layout): Observable<Layout> {
        return this._http.post<Layout>(this.api.concat('add'), layout)
    }

    list(): Observable<Layout[]> {
        return this._http.get<Layout[]>(this.api.concat('list'))
    }
}
