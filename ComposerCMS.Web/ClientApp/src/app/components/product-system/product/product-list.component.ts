import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import Product from '../../../models/Product';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  providers: [ProductService]
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private _productService: ProductService) {

  }

  ngOnInit() {
    this._productService.list().subscribe((products) => this.products = products);
  }
}
