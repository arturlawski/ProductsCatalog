import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Product } from 'src/app/product';
import { ToastrService } from "ngx-toastr";
import { forkJoin} from "rxjs";
import { Router } from "@angular/router";
import { ProductsService } from '../service/products.service';

@Component({
  selector: 'app-products-catalog',
  templateUrl: './products-catalog.component.html',
  styleUrls: ['./products-catalog.component.css']
})
export class ProductsCatalogComponent implements OnInit {
  products: Product[] = [];
  totalProductsCount: number;
  startIndex: number = 0;
  limit: number = 10;
  constructor(
    private productsService: ProductsService,
    private toastrService: ToastrService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {
  }

  ngOnInit(): void {
    forkJoin([
      this.productsService.getProductsCount(),
      this.productsService.getProducts(this.startIndex, this.limit)
    ]).subscribe({
      next: (([count, products]) => {
        this.totalProductsCount = count;
        this.products = products;
      })
    })
  }

  redirectToCreate(productId?: string) {
    this.router.navigate(['products', 'new']);
  }
  onPaginatorClicked(event: any) {
    this.startIndex = this.limit * event.pageIndex;
    this.productsService.getProducts(this.startIndex, this.limit).subscribe({
      next: (products) => {
        this.products = products;
        this.cdr.markForCheck();
      }
    });
  }

}
