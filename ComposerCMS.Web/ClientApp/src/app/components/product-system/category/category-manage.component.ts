import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../services/category.service';
import Category from '../../../models/Category';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category-manage',
  templateUrl: './category-manage.component.html',
  providers: [CategoryService]
})
export class CategoryManageComponent implements OnInit {
  categoryId: string;
  category: Category = new Category();

  constructor(private _activatedRoute: ActivatedRoute, private _categoryService: CategoryService) {
    this._activatedRoute.params.subscribe((params) => {
      this.categoryId = params.categoryId;
    });
  }

  ngOnInit() {
    this._categoryService.get(this.categoryId).subscribe((category) => this.category = category);
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
