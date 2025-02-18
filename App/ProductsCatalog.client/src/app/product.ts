export interface Product {
  id: string;
  code: string;
  name: string;
  price: number;
}

export interface CreateProduct {
  code: string;
  name: string;
  price: number;
}
