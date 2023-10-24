import { useEffect, useState } from "react";
import { Button, InputGroup, Tab, Tabs } from "react-bootstrap";
import { Topping, ToppingCategory } from "../../types/dataTypes";
import { getToppings } from "../../lib/api/topping";
import { getToppingCategories } from "../../lib/api/toppingCategory";
import {
  getToppingCount,
  updateToppingCount,
} from "../../lib/pizzaCreatorUtils";

export default function ToppingTab({
  currentStep,
  setActiveStep,
}: {
  currentStep: number;
  setActiveStep: (step: number) => void;
}) {
  const [categories, setCategories] = useState<ToppingCategory[]>([]);

  useEffect(() => {
    const fetch = async () => {
      setCategories(await getToppingCategories());
    };

    fetch();
  }, []);

  return (
    <div className="d-flex flex-column gap-3">
      <div className="h-100 w-100 d-flex justify-content-center">
        <h3>Choose pizza toppings</h3>
      </div>
      <i>10% discount if you choose more than 3 unique toppings</i>
      <Tabs>
        {categories.map((category) => {
          return (
            <Tab
              key={"tab" + category.id + category.name}
              eventKey={category.id}
              title={category.name}
            >
              <ToppingCategoryTab
                key={category.id + category.name}
                id={category.id}
              />
            </Tab>
          );
        })}
      </Tabs>
      <div className="d-flex gap-2 w-100 justify-content-end">
        <Button
          variant="secondary"
          onClick={() => setActiveStep(currentStep - 1)}
        >
          Change size
        </Button>
        <Button onClick={() => setActiveStep(currentStep + 1)}>Order</Button>
      </div>
    </div>
  );
}

function ToppingCategoryTab({ id }: { id: number }) {
  const [toppings, setToppings] = useState<Topping[]>([]);

  useEffect(() => {
    const fetch = async () => {
      setToppings(await getToppings(id));
    };

    fetch();
  }, [id]);

  return (
    <div className="d-flex gap-2 flex-wrap">
      {toppings.map((topping) => {
        return (
          <ToppingElement
            key={"ToppingElement" + topping.id}
            topping={topping}
          />
        );
      })}
    </div>
  );
}

function ToppingElement({ topping }: { topping: Topping }) {
  const [selectedCount, setSelectedCount] = useState(getToppingCount(topping));

  const handleAmountChange = (count: number) => {
    const newAmount = selectedCount + count;

    if (newAmount < 0 || newAmount > topping.limit) return;

    setSelectedCount(newAmount);
    updateToppingCount(topping, count);
  };

  return (
    <div className="d-flex flex-column border p-2 align-items-center rounded shadow-sm">
      <div>{topping.name}</div>
      <div>{topping.price}€</div>
      <InputGroup>
        <Button onClick={() => handleAmountChange(-1)}>-</Button>
        <InputGroup.Text>{selectedCount}</InputGroup.Text>
        <Button onClick={() => handleAmountChange(1)}>+</Button>
      </InputGroup>
    </div>
  );
}
