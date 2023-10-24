export interface Topping {
  id: number;
  categoryId: number;
  name: string;
  limit: number;
  price: number;
}

export interface StoredTopping {
  id: number;
  name: string;
  categoryId: number;
  count: number;
}

export interface ToppingCategory {
  id: number;
  name: string;
  toppings: Topping[];
}

export interface PizzaSize {
  id: number;
  name: string;
  order: number;
  price: number;
}

export interface Order {
  id: number;
  creationDate: Date;
  state: string;
  pizzaSize: PizzaSize;
  toppings: StoredTopping[];
}

export interface CreateOrder {
  lastUpdate: Date;
  pizzaSize: PizzaSize | null;
  isDraft: boolean;
  toppings: StoredTopping[];
}

export interface UpdateOrder {
  id: number;
  state: string;
}
