import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { Blog } from '../models/Blog';

@Injectable()
export class BlogService extends BaseService {
  get api() {
    return `${super.api}/api/account/`;
  }

  add(blog: Blog) {
    return this.http.post(this.api.concat('add'), blog);
  }

  update(blog: Blog) {
    return this.http.post(this.api.concat('update'), blog);
  }

  get(blogId: string): Observable<Blog> {
    return this.http.get<Blog>(this.api.concat(`get/${blogId}`));
  }

  list(): Observable<Blog[]> {
    return this.http.get<Blog[]>(this.api.concat('list'))
  }
}
