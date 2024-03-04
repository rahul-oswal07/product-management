import { Injectable, Injector } from '@angular/core';
import { HttpService } from './http-service';
import { Product } from './product';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProductService extends HttpService {
  override path = 'products';

  constructor(injector: Injector, private httpClient: HttpClient) {
    super(injector);
  }
  getProducts() {
    return this.getList<Product>();
  }
  getProductsByName(name: string) {
    return this.getList<Product>('search', { name: name });
  }
  addProduct(product: Product) {
    return this.post(product);
  }
  getProduct(id: number) {
    return this.getSingle<Product>(id.toString());
  }
  editProduct(product: Product) {
    return this.put(product);
  }
  deleteProduct(id: number) {
    return this.delete(id.toString());
  }
}
