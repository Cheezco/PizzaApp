import { Button } from "react-bootstrap";
import { CreateOrder } from "../../types/dataTypes";
import { useState } from "react";
import { postOrder } from "../../lib/api/order";

export default function OrderPlacedTab({
  order,
  setActiveStep,
}: {
  order: CreateOrder | null;
  setActiveStep: (step: number) => void;
}) {
  const [canSave, setCanSave] = useState(true);
  const [showSaveError, setShowSaveError] = useState(false);
  const [orderSaved, setOrderSaved] = useState(false);

  const handleSaveOrder = async () => {
    if (!order) {
      setCanSave(false);
      return;
    }
    order.isDraft = true;
    const success = await postOrder(order);

    if (!success) {
      setShowSaveError(true);
      setOrderSaved(false);
      return;
    }

    setShowSaveError(false);
    setCanSave(false);
    setOrderSaved(true);
  };

  return (
    <div className="d-flex flex-column justify-content-center gap-4 align-items-center">
      <h3>Order placed!</h3>
      {showSaveError && <p>Failed to save</p>}
      {orderSaved && <p>Order saved</p>}
      <div className="d-flex gap-2">
        <Button onClick={handleSaveOrder} disabled={!canSave}>
          Save order
        </Button>
        <Button onClick={() => setActiveStep(0)}>Create new pizza</Button>
      </div>
    </div>
  );
}
