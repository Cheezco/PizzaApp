import { Button } from "react-bootstrap";
import { CreateOrder } from "../../types/dataTypes";
import { useState } from "react";
import { postOrder } from "../../lib/api/order";
import { useNavigate } from "react-router-dom";

export default function OrderPlacedTab({
  order,
}: {
  order: CreateOrder | null;
}) {
  const [canSave, setCanSave] = useState(true);
  const [showSaveError, setShowSaveError] = useState(false);
  const [orderSaved, setOrderSaved] = useState(false);
  const navigate = useNavigate();

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
        <Button onClick={() => navigate("/pizza-creator")}>
          Create new pizza
        </Button>
      </div>
    </div>
  );
}
