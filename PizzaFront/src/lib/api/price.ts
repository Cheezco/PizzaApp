import API from "../../api";
import { CreateOrder } from "../../types/dataTypes";
import { PriceRequest, PriceResponse } from "../../types/priceRequestTypes";

async function getPrice(order: CreateOrder): Promise<PriceResponse | null> {
  const priceRequest: PriceRequest = {
    pizzaSizeId: order.pizzaSize?.id ?? 0,
    toppings: order.toppings,
  };
  const response = await API.post<PriceResponse>("price", priceRequest);

  if (response.status !== 200) {
    return null;
  }

  return response.data;
}

export { getPrice };
