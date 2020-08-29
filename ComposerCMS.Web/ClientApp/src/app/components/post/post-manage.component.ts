import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Post } from '../../models/Post';
import { PostService } from '../../services/post.service';

@Component({
  selector: 'app-post-manage',
  templateUrl: './post-manage.component.html',
  providers: [PostService]
})
export class PostManageComponent implements OnInit {
  post: Post = new Post();

  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private _postService: PostService) {
    this._activatedRoute.params.subscribe((params) => {
      this.post.blogId = params.blogId;
      this.post.id = params.postId;
    })
  }

  ngOnInit(): void {
    if (this.post.id) {
      this._postService.get(this.post.id).subscribe((post) => this.post = post);
    }
  }

  save(): void {
    if (this.post.id) {
      this._postService.update(this.post).subscribe(() => {
        alert('Page updated successfully');
      });
    }
    else {
      this._postService.add(this.post).subscribe(() => {
        this._router.navigateByUrl('blogs');
      });
    }
  }
}
