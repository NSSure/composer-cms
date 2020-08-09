import { Component, OnInit } from '@angular/core';
import { BlogService } from '../services/blog.service';
import { Blog } from '../../models/Blog';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  providers: [BlogService]
})
export class BlogListComponent implements OnInit {
  isLoading: boolean = true;
  blogs: Array<Blog> = new Array();

  constructor(private _blogService: BlogService) {

  }

  ngOnInit(): void {
    this._blogService.list().subscribe((blogs) => {
      this.blogs = blogs;
      this.isLoading = false;
    });
  }
}
