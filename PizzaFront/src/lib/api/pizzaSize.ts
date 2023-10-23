import API from "../../api";
import { PizzaSize } from "../../types/dataTypes";

async function getPizzaSizes(): Promise<PizzaSize[]> {
  const response = await API.get<PizzaSize[]>("pizza-sizes");

  if (response.status !== 200) {
    return [];
  }

  const data = response.data as PizzaSize[];
  data.sort((a, b) => a.order - b.order);

  return data;
}

export { getPizzaSizes };
