import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { Blog } from '../../models/Blog';

@Injectable()
export class BlogService {
  api = 'http://localhost:51494/api/blog/';

  constructor(private _http: HttpClient) { }

  add(blog: Blog) {
    return this._http.post(this.api.concat('add'), blog);
  }

  update(blog: Blog) {
    return this._http.post(this.api.concat('update'), blog);
  }

  get(blogId: string): Observable<Blog> {
    return this._http.get<Blog>(this.api.concat(`get/${blogId}`));
  }

  list(): Observable<Blog[]> {
    return this._http.get<Blog[]>(this.api.concat('list'))
  }
}
