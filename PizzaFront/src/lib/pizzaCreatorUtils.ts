import { CreateOrder, PizzaSize, Topping } from "../types/dataTypes";

const key = "pizza-creator";
const timeSpan = 60 * 10;

function getOrderData() {
  const storedDataString = sessionStorage.getItem(key);

  let storedData: CreateOrder = {
    lastUpdate: new Date(),
    pizzaSize: null,
    toppings: [],
  };

  if (storedDataString !== null) {
    const temp: CreateOrder = JSON.parse(storedDataString);

    if (typeof temp !== "undefined") {
      temp.lastUpdate = new Date(temp.lastUpdate);

      if (
        (temp.lastUpdate.getTime() - new Date().getTime()) / 1000 <
        timeSpan
      ) {
        storedData = temp;
      }
    }
  }

  return storedData;
}

function saveStoredObject(obj: CreateOrder) {
  obj.lastUpdate = new Date();
  sessionStorage.setItem(key, JSON.stringify(obj));
}

function removeOrderData() {
  sessionStorage.removeItem(key);
}

function saveSelectedSize(size: PizzaSize) {
  const storedData = getOrderData();

  storedData.pizzaSize = size;
  saveStoredObject(storedData);
}

function getSelectedSize(): PizzaSize | null {
  const storedData = getOrderData();

  return storedData.pizzaSize;
}

function updateToppingCount(topping: Topping, count: number) {
  const storedData = getOrderData();

  const storedTopping = storedData.toppings.find((x) => x.id == topping.id);

  if (typeof storedTopping === "undefined") {
    storedData.toppings.push({
      id: topping.id,
      categoryId: topping.categoryId,
      count: count,
      name: topping.name,
    });
  } else {
    storedTopping.count += count;

    if (storedTopping.count <= 0) {
      storedData.toppings = storedData.toppings.filter(
        (x) => x.id !== topping.id
      );
    }
  }

  saveStoredObject(storedData);
}

function getToppingCount(topping: Topping): number {
  const storedData = getOrderData();

  const storedTopping = storedData.toppings.find((x) => x.id == topping.id);

  return typeof storedTopping === "undefined" ? 0 : storedTopping.count;
}

export {
  saveSelectedSize,
  getSelectedSize,
  updateToppingCount,
  getToppingCount,
  removeOrderData,
  getOrderData,
};
