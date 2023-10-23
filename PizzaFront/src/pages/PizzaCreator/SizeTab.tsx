import { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import "./style.css";
import { PizzaSize } from "../../types/dataTypes";

import { getPizzaSizes } from "../../lib/api/pizzaSize";
import { getSelectedSize, saveSelectedSize } from "../../lib/pizzaCreatorUtils";

export default function SizeTab({
  currentStep,
  setActiveStep,
}: {
  currentStep: number;
  setActiveStep: (step: number) => void;
}) {
  const [pizzaSizes, setPizzaSizes] = useState<PizzaSize[]>([]);

  useEffect(() => {
    const fetch = async () => {
      setPizzaSizes(await getPizzaSizes());
    };

    fetch();
  }, []);

  const [selectedSize, setSelectedSize] = useState(getSelectedSize()?.id ?? -1);

  const handleSizeSelect = (size: PizzaSize) => {
    setSelectedSize(size.id);
    saveSelectedSize(size);
  };

  return (
    <div className="p-4 gap-4 d-flex flex-column h-100">
      <div className="d-flex w-100 h-100 justify-content-center flex-column align-items-center gap-3">
        <div className="h-100 w-100 d-flex justify-content-center">
          <h3>Choose pizza size</h3>
        </div>
        <div className="d-flex justify-content-center align-items-center gap-2">
          {pizzaSizes.map((size, index) => {
            return (
              <Button
                key={size.id}
                onClick={() => handleSizeSelect(size)}
                className={`${
                  selectedSize == size.id
                    ? "selected-size-option"
                    : "size-option"
                } rounded-circle d-flex justify-content-center align-items-center text-black`}
                style={{ width: `${5 + index}em`, height: `${5 + index}em` }}
              >
                <div>
                  <div>{size.name}</div>
                  <div>{size.price}€</div>
                </div>
              </Button>
            );
          })}
        </div>
      </div>
      <div className="d-flex justify-content-end">
        <Button
          onClick={() => setActiveStep(currentStep + 1)}
          disabled={selectedSize === -1}
        >
          Choose toppings
        </Button>
      </div>
    </div>
  );
}
