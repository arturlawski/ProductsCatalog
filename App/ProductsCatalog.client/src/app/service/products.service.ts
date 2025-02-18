import { Observable } from "rxjs";
import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { CreateProduct, Product } from "../product";

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  productsApiEndpoint :string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.productsApiEndpoint = baseUrl + 'api/products';
  }

  getProducts(startIndex: number, limit: number): Observable<Product[]> {
    let params = new HttpParams();
    params = params.set('startIndex', startIndex.toString());
    params = params.set('limit', limit.toString());
    return this.http.get<Product[]>(`${this.productsApiEndpoint}`, {params: params});
  }

  createProduct(request: CreateProduct): Observable<Product> {
    return this.http.post<Product>(`${this.productsApiEndpoint}`, request);
  }

  getProductsCount(): Observable<number> {
    return this.http.get<number>(`${this.productsApiEndpoint}/count`);
  }
}
