import { Injectable } from '@angular/core';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { Post } from '../models/Post';

@Injectable()
export class PostService extends BaseService {
  get api() {
    return `${super.api}/api/post/`;
  }

  add(post: Post) {
    return this.http.post(this.api.concat('add'), post);
  }

  update(post: Post) {
    return this.http.post(this.api.concat('update'), post);
  }

  get(postID: string): Observable<Post> {
    return this.http.get<Post>(this.api.concat(`get/${postID}`));
  }

  list(blogId: string): Observable<Post[]> {
    return this.http.get<Post[]>(this.api.concat(`list/${blogId}`));
  }
}
