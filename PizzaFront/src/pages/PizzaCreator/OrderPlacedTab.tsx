import { useState } from "react";
import { Button } from "react-bootstrap";
import { CreateOrder } from "../../types/dataTypes";
import { postOrder } from "../../lib/api/order";

export default function OrderPlacedTab({ order }: { order: CreateOrder }) {
  const [disableSaveButton, setDisableSaveButton] = useState(false);

  const handleSaveOrder = async () => {
    order.isDraft = true;
    await postOrder(order);
    setDisableSaveButton(true);
  };

  return (
    <div className="d-flex flex-column justify-content-center gap-4 align-items-center">
      <h3>Order placed!</h3>
      <div className="d-flex gap-2">
        <Button onClick={handleSaveOrder} disabled={disableSaveButton}>
          Save order
        </Button>
        <Button>Create new pizza</Button>
      </div>
    </div>
  );
}
