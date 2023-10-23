import { StoredTopping } from "./dataTypes";

export interface PriceRequest {
  pizzaSizeId: number;
  toppings: StoredTopping[];
}

export interface PriceResponseTopping {
  id: number;
  categoryId: number;
  count: number;
  price: number;
}

export interface PriceResponse {
  totalPrice: number;
  sizePrice: number;
  discountApplied: boolean;
  toppings: PriceResponseTopping[];
}
