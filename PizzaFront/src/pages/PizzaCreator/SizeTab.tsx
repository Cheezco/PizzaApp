import { useState } from "react";
import { Button } from "react-bootstrap";
import "./style.css";

export default function SizeTab({
  currentStep,
  setActiveStep,
}: {
  currentStep: number;
  setActiveStep: (step: number) => void;
}) {
  const sizes = getPizzaSizes();
  const [selectedSize, setSelectedSize] = useState(0);

  return (
    <div className="p-4 gap-4 d-flex flex-column h-100">
      <div className="d-flex w-100 h-100 justify-content-center flex-column align-items-center gap-3">
        <div className="h-100 w-100 d-flex justify-content-center">
          <h3>Choose pizza size</h3>
        </div>
        <div className="d-flex justify-content-center align-items-center gap-2">
          {sizes.map((size, index) => {
            return (
              <Button
                onClick={() => setSelectedSize(index)}
                className={`${
                  selectedSize == index ? "selected-size-option" : "size-option"
                } rounded-circle d-flex justify-content-center align-items-center text-black`}
                style={size.renderSize}
              >
                {size.text}
              </Button>
            );
          })}
        </div>
      </div>
      <div className="d-flex justify-content-end">
        <Button onClick={() => setActiveStep(currentStep + 1)}>
          Choose toppings
        </Button>
      </div>
    </div>
  );
}

function getPizzaSizes() {
  return [
    {
      text: "Small",
      renderSize: { width: "5em", height: "5em" },
    },
    {
      text: "Medium",
      renderSize: { width: "6em", height: "6em" },
    },
    {
      text: "Large",
      renderSize: { width: "6.5em", height: "6.5em" },
    },
  ];
}
