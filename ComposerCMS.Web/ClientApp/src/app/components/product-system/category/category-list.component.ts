import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../services/category.service';
import Category from '../../../models/Category';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  providers: [CategoryService]
})
export class CategoryListComponent implements OnInit {
  categories: Array<Category> = [];

  constructor(private _categoryService: CategoryService) {

  }

  ngOnInit() {
    this._categoryService.list().subscribe((categories) => this.categories = categories);
  }
}
