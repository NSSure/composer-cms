import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { ActivatedRoute } from '@angular/router';
import Product from '../../../models/Product';
import { CategoryService } from '../../../services/category.service';
import Category from '../../../models/Category';

@Component({
  selector: 'app-product-manage',
  templateUrl: './product-manage.component.html',
  providers: [ProductService, CategoryService]
})
export class ProductManageComponent implements OnInit {
  product: Product = new Product();
  productId: string;

  categories: Category[] = [];

  categoryMap: any = {};

  classification: string = 'individual';

  constructor(private _activatedRoute: ActivatedRoute, private _productService: ProductService, private _categoryService: CategoryService) {
    this._activatedRoute.params.subscribe((params) => {
      this.productId = params.productId;
    })
  }

  ngOnInit() {
    this._productService.get(this.productId).subscribe((product) => this.product = product);
    this._categoryService.list().subscribe((categories) => this.categories = categories);
  }

  save() {
    this.product.hasVariants = this.classification === 'variant';

    if (this.product.id) {
      // update
    }
    else {
      // add
      this._productService.add(this.product).subscribe(() => {
        alert('Product created successfully');
      })
    }
  }
}
