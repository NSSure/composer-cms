import { Component, OnInit } from '@angular/core';
import { BlogService } from '../services/blog.service';
import { Blog } from '../../models/Blog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blog-manage',
  templateUrl: './blog-manage.component.html',
  providers: [BlogService]
})
export class BlogManageComponent implements OnInit {
  blog: Blog = new Blog();

  constructor(private _router: Router, private _blogService: BlogService) {

  }

  ngOnInit(): void {

  }

  save(): void {
    this._blogService.add(this.blog).subscribe(() => {
      this._router.navigateByUrl('blogs');
    });
  }
}
