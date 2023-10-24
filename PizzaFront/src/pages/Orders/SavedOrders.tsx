import { useEffect, useState } from "react";
import { Accordion, Button, Modal } from "react-bootstrap";
import { Order } from "../../types/dataTypes";
import { deleteOrder, getOrders } from "../../lib/api/order";

export default function SavedOrders() {
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [draftOrders, setDraftOrders] = useState<Order[]>([]);
  const fetch = async () => {
    setDraftOrders(await getOrders(true));
  };

  useEffect(() => {
    fetch();
  }, []);

  const handleDeleteOrder = async (id: number) => {
    await deleteOrder(id);
    fetch();
  };

  return (
    <div className="d-flex flex-column w-100">
      <Accordion className="p-4">
        {draftOrders.map((order) => {
          return (
            <Accordion.Item
              key={"DraftOrder" + order.id}
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
                <div className="d-flex gap-2 justify-content-end">
                  <Button
                    disabled={true}
                    variant="danger"
                    onClick={() => setShowDeleteModal(true)}
                  >
                    Delete
                  </Button>
                  <DeleteModal
                    show={showDeleteModal}
                    closeModal={() => setShowDeleteModal(false)}
                    deleteOrder={() => {
                      setShowDeleteModal(false);
                      handleDeleteOrder(order.id);
                    }}
                  />
                  <Button disabled={true}>Order</Button>
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
