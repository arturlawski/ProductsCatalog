import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { filter, map, Subject, switchMap, takeUntil } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { CreateProduct, Product } from '../product';
import { ProductsService } from '../service/products.service';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {
  product: Product;
  showErrors = false;
  ngUnsubscribe = new Subject<void>();

  constructor(
    private route: ActivatedRoute,
    private productsService: ProductsService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.route.params.pipe(
      takeUntil(this.ngUnsubscribe),
      map(params => params['id']),
      filter(id => !!id),
    ).subscribe({
      next: (product => {
        this.product = product;
        this.patchForm();
      })
    })
  }


  formGroup = this.formBuilder.group({
    code: new FormControl<string>('', {validators: [Validators.required, Validators.minLength(1)]}),
    name: new FormControl<string>('', {validators: [Validators.required, Validators.minLength(1)]}),
    price: new FormControl<number | null>(null, {validators: [Validators.required, Validators.min(1)]})
  });

  submit() {
    this.formGroup.markAllAsTouched();
    this.showErrors = true;
    if (!this.formGroup.valid) {
      return;
    }
    this.createProduct();
  }

  redirectToCatalog() {
    this.router.navigate(['browser']);
  }

  private createProduct() {
    this.productsService.createProduct(this.getCreateProductRequest()).subscribe({
      next: (_) => {
        this.toastr.success('Produkt dodany');
        this.router.navigate(['products']);
      }
    })
  }

  private patchForm() {
    this.formGroup.patchValue({
      code: this.product.code,
      name: this.product.name,
      price: this.product.price
    })
  }
  
  private getCreateProductRequest(): CreateProduct {
    return {
      code: this.formGroup.controls.code.value!,
      name: this.formGroup.controls.name.value!,
      price: this.formGroup.controls.price.value!
    };
  }


}
