import { Button } from "react-bootstrap";

export default function OrderTab({
  currentStep,
  setActiveStep,
}: {
  currentStep: number;
  setActiveStep: (step: number) => void;
}) {
  const order = getOrder();
  return (
    <div>
      <div className="h-100 w-100 d-flex justify-content-center">
        <h3>Verify order</h3>
      </div>
      <div className="w-100 d-flex justify-content-between">
        <h5>
          <b>Size:</b> {order.size}
        </h5>
        <b>0.00</b>
      </div>
      <hr />
      <div className="d-flex flex-column gap-2">
        <h5>
          <b>Toppings:</b>
        </h5>
        <div className="d-flex flex-column">
          {order.toppings.map((topping) => {
            return (
              <>
                <div className="d-flex gap-2 justify-content-between">
                  <div>
                    {topping.name} x{topping.count}
                  </div>
                  <b>0.00</b>
                </div>
                <hr />
              </>
            );
          })}
        </div>
      </div>
      <h4>
        <b>Total price: 0.00</b>
      </h4>
      <div className="d-flex justify-content-end gap-2">
        <Button
          variant="secondary"
          onClick={() => setActiveStep(currentStep - 1)}
        >
          Change toppings
        </Button>
        <Button>Place order</Button>
      </div>
    </div>
  );
}

function getOrder() {
  return {
    size: "Large",
    toppings: [
      {
        id: 1,
        name: "Cheddar",
        count: 2,
      },
      {
        id: 3,
        name: "Bacon",
        count: 1,
      },
      {
        id: 4,
        name: "Beef",
        count: 3,
      },
    ],
  };
}
