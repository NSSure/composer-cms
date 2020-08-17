import { Component, OnInit } from '@angular/core';
import { BlogService } from '../services/blog.service';
import { Blog } from '../../models/Blog';
import { Router, ActivatedRoute } from '@angular/router';

import SureToastManager from '../../common/toast';

@Component({
  selector: 'app-blog-manage',
  templateUrl: './blog-manage.component.html',
  providers: [BlogService]
})
export class BlogManageComponent implements OnInit {
  blogId: string;
  blog: Blog = new Blog();
  toast = SureToastManager();

  constructor(private _activatedRoute: ActivatedRoute, private _router: Router, private _blogService: BlogService) {
    this._activatedRoute.params.subscribe((params) => {
      this.blogId = params.blogId;
    });
  }

  ngOnInit(): void {
    this._blogService.get(this.blogId).subscribe((blog) => this.blog = blog);

  }

  save(): void {
    if (this.blog.id) {
      this._blogService.update(this.blog).subscribe(() => {
        this.toast.showSuccess('Blog updated successfully', {});
      });
    }
    else {
      this._blogService.add(this.blog).subscribe(() => {
        this.toast.showSuccess('Blog added successfully', {});
      });
    }
  }
}
