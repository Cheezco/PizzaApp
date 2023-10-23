import { Accordion } from "react-bootstrap";

export default function OrderHistory() {
  const orders = getOrders();

  return (
    <>
      <>
        <Accordion className="p-4">
          {orders.map((order) => {
            return (
              <Accordion.Item eventKey={order.id.toString()}>
                <Accordion.Header>Order#{order.id}</Accordion.Header>
                <Accordion.Body>
                  <div>
                    <b>State:</b> {order.state}
                  </div>
                  <hr />
                  <div>
                    <b>Size:</b> {order.size}
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
                  <div>
                    <b>Price:</b> {order.price}
                  </div>
                </Accordion.Body>
              </Accordion.Item>
            );
          })}
        </Accordion>
      </>
    </>
  );
}

function getOrders() {
  return [
    {
      id: 1,
      lastOrder: "2023/01/01",
      size: "Large",
      price: "0.00",
      state: "Draft",
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
    },
    {
      id: 2,
      lastOrder: "2023/01/01",
      size: "Large",
      price: "0.00",
      state: "Draft",
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
    },
    {
      id: 3,
      lastOrder: "2023/01/01",
      size: "Large",
      price: "0.00",
      state: "Confirmed",
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
    },
  ];
}
