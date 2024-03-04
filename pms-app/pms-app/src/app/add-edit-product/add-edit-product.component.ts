import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { ProductService } from '../services/product-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map, of, switchMap } from 'rxjs';
import { Product } from '../services/product';

@Component({
  selector: 'app-add-edit-product',
  templateUrl: './add-edit-product.component.html',
  styleUrls: ['./add-edit-product.component.css'],
})
export class AddEditProductComponent {
  productForm: FormGroup;
  title: string = 'Add Product';
  constructor(
    private formBuilder: FormBuilder,
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.productForm = new UntypedFormGroup({
      id: new FormControl<number>(0),
      name: new UntypedFormControl('', {
        validators: [Validators.required],
      }),
      price: new FormControl<number>(0),
      description: new UntypedFormControl('', {
        validators: [Validators.required],
      }),
      category: new UntypedFormControl('', {
        validators: [Validators.required],
      }),
    });
  }

  ngOnInit(): void {
    this.activatedRoute.params
      .pipe(
        map((params) => params['id']),
        switchMap((id) =>
          id ? this.productService.getProduct(id) : of(new Product())
        )
      )
      .subscribe({
        next: (product) => {
          this.productForm.reset();
          this.productForm.patchValue(product);
          this.title = product.id
            ? `Edit Category - ${product.name}`
            : 'Create New Category';
        },
        error: (e: Error) => {
          console.error(e);
        },
      });
  }

  saveProduct() {
    let observer: Observable<void>;
    if (this.productForm.get('id')?.value) {
      observer = this.productService.editProduct(this.productForm.value);
    } else {
      observer = this.productService.addProduct(this.productForm.value);
    }
    observer.subscribe({
      next: () => {
        this.productForm.reset();
        this.router.navigate(['/products']);
      },
      error: (e: Error) => {
        this.productForm.markAsDirty();
      },
    });
  }
}
