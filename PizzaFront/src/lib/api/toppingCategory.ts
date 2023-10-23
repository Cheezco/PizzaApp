import API from "../../api";
import { ToppingCategory } from "../../types/dataTypes";

async function getToppingCategories(): Promise<ToppingCategory[]> {
  const response = await API.get<ToppingCategory[]>("topping-categories");

  if (response.status !== 200) {
    return [];
  }

  return response.data;
}

export { getToppingCategories };
