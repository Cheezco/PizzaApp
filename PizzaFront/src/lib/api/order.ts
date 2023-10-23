import API from "../../api";
import { CreateOrder } from "../../types/dataTypes";

async function postOrder(order: CreateOrder): Promise<boolean> {
  const response = await API.post("orders", order);

  return response.status === 201;
}

export { postOrder };
