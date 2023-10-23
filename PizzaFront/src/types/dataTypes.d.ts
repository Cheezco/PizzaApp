export interface Topping {
  id: number;
  categoryId: number;
  name: string;
  limit: number;
  price: number;
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
  toppings: Topping[];
}

export interface CreateOrder {
  pizzaSize: PizzaSize;
  toppings: Topping[];
}

export interface UpdateOrder {
  id: number;
  state: string;
}
