import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

// Import RxJs required methods
import { Observable } from 'rxjs';
import { Post } from '../../models/Post';

@Injectable()
export class PostService {
  api = 'http://localhost:51494/api/post/';

  constructor(private _http: HttpClient) { }

  add(post: Post) {
    return this._http.post(this.api.concat('add'), post);
  }

  update(post: Post) {
    return this._http.post(this.api.concat('update'), post);
  }

  get(postID: string): Observable<Post> {
    return this._http.get<Post>(this.api.concat(`get/${postID}`));
  }

  list(blogId: string): Observable<Post[]> {
    return this._http.get<Post[]>(this.api.concat(`list/${blogId}`));
  }
}
