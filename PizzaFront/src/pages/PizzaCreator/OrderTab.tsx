import { useEffect, useState } from "react";
import { Button, Toast } from "react-bootstrap";
import { getPrice } from "../../lib/api/price";
import { PriceResponse } from "../../types/priceRequestTypes";
import { getOrderData, removeOrderData } from "../../lib/pizzaCreatorUtils";
import { postOrder } from "../../lib/api/order";
import { getToken } from "../../lib/authUtils";
import { useJwt } from "react-jwt";
import { JwtToken } from "../../types/authTypes";
import { CreateOrder } from "../../types/dataTypes";

export default function OrderTab({
  currentStep,
  setActiveStep,
  setOrder,
}: {
  currentStep: number;
  setActiveStep: (step: number) => void;
  setOrder: (order: CreateOrder) => void;
}) {
  const [priceData, setPriceData] = useState<PriceResponse | null>(null);
  const order = getOrderData();
  const { decodedToken, isExpired, reEvaluateToken } = useJwt<JwtToken>(
    getToken()
  );
  const [loggedIn, setLoggedIn] = useState(false);

  useEffect(() => {
    const interval = setInterval(() => {
      reEvaluateToken(getToken());

      if (!decodedToken || isExpired) {
        setLoggedIn(false);
        return;
      }

      setLoggedIn(true);
    }, 100);

    return () => clearInterval(interval);
  }, [decodedToken, isExpired, reEvaluateToken]);

  useEffect(() => {
    const fetch = async () => {
      setPriceData(await getPrice(order));
    };

    fetch();
  }, []);

  const handlePlaceOrder = async () => {
    const success = await postOrder(order);

    if (success) {
      setOrder(order);
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
      {!loggedIn && <div>Login or register to place an order</div>}
      <div className="d-flex justify-content-end gap-2">
        <Button
          variant="secondary"
          onClick={() => setActiveStep(currentStep - 1)}
        >
          Change toppings
        </Button>
        <Button onClick={handlePlaceOrder} disabled={!loggedIn}>
          Place order
        </Button>
      </div>
    </div>
  );
}
