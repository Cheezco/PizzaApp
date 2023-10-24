import { useEffect, useState } from "react";
import { Accordion } from "react-bootstrap";
import { getOrders } from "../../lib/api/order";
import { Order } from "../../types/dataTypes";

export default function OrderHistory() {
  const [orders, setOrders] = useState<Order[]>([]);

  useEffect(() => {
    const fetch = async () => {
      setOrders(await getOrders());
    };
    fetch();
  }, []);

  return (
    <>
      <>
        <Accordion className="p-4">
          {orders.map((order) => {
            return (
              <Accordion.Item
                key={"Order" + order.id}
                eventKey={order.id.toString()}
              >
                <Accordion.Header>Order#{order.id}</Accordion.Header>
                <Accordion.Body>
                  <div>
                    <b>Size:</b> {order.pizzaSize.name}
                  </div>
                  <hr />
                  <div className="d-flex flex-column gap-2">
                    <b>Toppings</b>
                    <div>
                      {order.toppings.map((topping) => {
                        return (
                          <div>
                            {topping.name} x{topping.count}
                          </div>
                        );
                      })}
                    </div>
                  </div>
                  <hr />
                </Accordion.Body>
              </Accordion.Item>
            );
          })}
        </Accordion>
      </>
    </>
  );
}
