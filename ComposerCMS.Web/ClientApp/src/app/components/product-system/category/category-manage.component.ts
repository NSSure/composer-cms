import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../services/category.service';
import Category from '../../../models/Category';

@Component({
  selector: 'app-category-manage',
  templateUrl: './category-manage.component.html',
  providers: [CategoryService]
})
export class CategoryManageComponent implements OnInit {
  category: Category = new Category();

  constructor(private _categoryService: CategoryService) {

  }

  ngOnInit() {

  }

  save() {
    if (this.category.id) {
      this._categoryService.update(this.category).subscribe(() => {
        alert('Product category updated.');
      });
    }
    else {
      this._categoryService.add(this.category).subscribe(() => {
        alert('Product category added.');
      });
    }
  }
}
