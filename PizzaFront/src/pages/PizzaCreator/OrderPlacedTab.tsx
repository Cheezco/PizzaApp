import { Button } from "react-bootstrap";

export default function OrderPlacedTab() {
  return (
    <div className="d-flex flex-column justify-content-center gap-4 align-items-center">
      <h3>Order placed!</h3>
      <div className="d-flex gap-2">
        <Button>Save order</Button>
        <Button>Create new pizza</Button>
      </div>
    </div>
  );
}
