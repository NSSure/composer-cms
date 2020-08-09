import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { Post } from '../../models/Post';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  providers: [PostService]
})
export class PostListComponent implements OnInit {
  blogId: string;
  posts: Array<Post> = new Array();

  constructor(private _activatedRoute: ActivatedRoute, private _postService: PostService) {
    this._activatedRoute.params.subscribe((params) => {
      this.blogId = params.blogId;
    });
  }

  ngOnInit(): void {
    this._postService.list(this.blogId).subscribe((posts) => this.posts = posts);
  }
}
