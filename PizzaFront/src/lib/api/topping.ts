import API from "../../api";
import { Topping } from "../../types/dataTypes";

async function getToppings(categoryId: number): Promise<Topping[]> {
  const response = await API.get<Topping[]>(
    `topping-categories/${categoryId}/toppings`
  );

  if (response.status !== 200) {
    return [];
  }

  return response.data;
}

export { getToppings };
