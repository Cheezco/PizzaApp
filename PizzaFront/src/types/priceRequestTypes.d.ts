export interface PriceRequestTopping {
  id: number;
  categoryId: number;
  count: number;
}

export interface PriceRequest {
  pizzaSizeId: number;
  toppings: PriceRequestTopping[];
}
