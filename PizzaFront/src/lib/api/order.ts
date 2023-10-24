import API from "../../api";
import { CreateOrder, Order } from "../../types/dataTypes";
import { getAxiosConfig } from "../authUtils";

async function postOrder(order: CreateOrder): Promise<boolean> {
  const response = await API.post("orders", order, getAxiosConfig());

  return response.status === 201;
}

async function getOrders(draftOrders: boolean = false): Promise<Order[]> {
  const response = await API.get<Order[]>(
    `orders?isDraft=${draftOrders}`,
    getAxiosConfig()
  );

  if (response.status !== 200) {
    return [];
  }

  return response.data;
}

async function deleteOrder(id: number) {
  await API.delete(`orders/${id}`);
}

export { postOrder, getOrders, deleteOrder };
