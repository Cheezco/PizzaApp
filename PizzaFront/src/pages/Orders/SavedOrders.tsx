import { useState } from "react";
import { Accordion, Button, Modal } from "react-bootstrap";
import { Order } from "../../types/dataTypes";

export default function SavedOrders() {
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [draftOrders, setDraftOrders] = useState<Order[]>([]);
  const orders = getOrders();

  return (
    <div className="d-flex flex-column w-100">
      <Accordion className="p-4">
        {orders.map((order) => {
          return (
            <Accordion.Item eventKey={order.id.toString()}>
              <Accordion.Header>Order#{order.id}</Accordion.Header>
              <Accordion.Body>
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
                <div className="d-flex gap-2 justify-content-end">
                  <Button
                    variant="danger"
                    onClick={() => setShowDeleteModal(true)}
                  >
                    Delete
                  </Button>
                  <DeleteModal
                    show={showDeleteModal}
                    closeModal={() => setShowDeleteModal(false)}
                    deleteOrder={() => setShowDeleteModal(false)}
                  />
                  <Button>Order</Button>
                </div>
              </Accordion.Body>
            </Accordion.Item>
          );
        })}
      </Accordion>
    </div>
  );
}

function DeleteModal({
  show,
  closeModal,
  deleteOrder,
}: {
  show: boolean;
  closeModal: () => void;
  deleteOrder: () => void;
}) {
  return (
    <Modal show={show} onHide={closeModal}>
      <Modal.Header closeButton>
        Are you sure you want to delete your saved order?
      </Modal.Header>
      <Modal.Footer>
        <Button variant="secondary" onClick={closeModal}>
          Close
        </Button>
        <Button variant="danger" onClick={deleteOrder}>
          Delete
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

function getOrders() {
  return [
    {
      id: 1,
      lastOrder: "2023/01/01",
      size: "Large",
      price: "0.00",
      State: "Draft",
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
      State: "Draft",
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
      State: "Confirmed",
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
