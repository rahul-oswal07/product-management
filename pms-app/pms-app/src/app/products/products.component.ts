import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product-service';
import { Product } from '../services/product';
import { Observable, filter, map, of } from 'rxjs';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  filterName: string = '';
  categoryFilter: string = '';
  public products$: Observable<Product[]> = of([]);
  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.products$ = this.productService.getProducts();
  }
  deleteCategory(id: number) {
    this.productService.deleteProduct(id).subscribe(() => {
      this.products$ = this.productService.getProducts();
    });
  }
  filterProducts() {
    this.products$ = this.productService.getProductsByName(this.filterName);
  }
  resetFilter() {
    this.filterName = '';
    this.products$ = this.productService.getProducts();
  }
}
