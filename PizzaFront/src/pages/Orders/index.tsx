import { Tab, Tabs } from "react-bootstrap";
import SavedOrders from "./SavedOrders";
import OrderHistory from "./OrderHistory";

export function Orders() {
  return (
    <div className="d-flex flex-column w-100 h-100 align-items-center pt-4">
      <div className="w-50">
        <Tabs>
          <Tab eventKey="savedOrders" title="Saved orders">
            <SavedOrders />
          </Tab>
          <Tab eventKey="orderHistory" title="Order history">
            <OrderHistory />
          </Tab>
        </Tabs>
      </div>
    </div>
  );
}
