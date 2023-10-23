import { useEffect, useState } from "react";
import { Button, Toast } from "react-bootstrap";
import { getPrice } from "../../lib/api/price";
import { PriceResponse } from "../../types/priceRequestTypes";
import { getOrderData, removeOrderData } from "../../lib/pizzaCreatorUtils";
import { postOrder } from "../../lib/api/order";

export default function OrderTab({
  currentStep,
  setActiveStep,
}: {
  currentStep: number;
  setActiveStep: (step: number) => void;
}) {
  const [priceData, setPriceData] = useState<PriceResponse | null>(null);
  const order = getOrderData();

  useEffect(() => {
    const fetch = async () => {
      setPriceData(await getPrice(order));
    };

    fetch();
  }, []);

  const handlePlaceOrder = async () => {
    const success = await postOrder(order);

    if (success) {
      removeOrderData();
      setActiveStep(currentStep + 1);
    }
  };

  return (
    <div>
      <div className="h-100 w-100 d-flex justify-content-center">
        <h3>Verify order</h3>
      </div>
      <div className="w-100 d-flex justify-content-between">
        <h5>
          <b>Size:</b> {order?.pizzaSize?.name}
        </h5>
        <b>{priceData?.sizePrice ?? 0}€</b>
      </div>
      <hr />
      <div className="d-flex flex-column gap-2">
        <h5>
          <b>Toppings:</b>
        </h5>
        <div className="d-flex flex-column">
          {order?.toppings.map((topping) => {
            return (
              <>
                <div
                  key={"topping" + topping.id}
                  className="d-flex gap-2 justify-content-between"
                >
                  <div>
                    {topping.name} x{topping.count}
                  </div>
                  <b>
                    {priceData?.toppings.find((x) => x.id == topping.id)
                      ?.price ?? 0}
                    €
                  </b>
                </div>
                <hr />
              </>
            );
          })}
        </div>
      </div>
      {priceData?.discountApplied && <div>10% discount applied</div>}
      <h4>
        <b>Total price: {priceData?.totalPrice ?? 0}€</b>
      </h4>
      <div className="d-flex justify-content-end gap-2">
        <Button
          variant="secondary"
          onClick={() => setActiveStep(currentStep - 1)}
        >
          Change toppings
        </Button>
        <Button onClick={handlePlaceOrder}>Place order</Button>
      </div>
    </div>
  );
}
