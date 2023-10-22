import { Button } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";

export function Home() {
  return (
    <div className="w-100 h-75 d-flex justify-content-center align-items-center flex-column gap-2">
      <div className="d-flex flex-column justify-content-center align-items-center gap-2">
        <div className="d-flex gap-2 align-items-center">
          <img src="./images/pizza.svg" height={120} width={120} />
          <h1 className="display-1">PizzaMe</h1>
          <img src="./images/pizza.svg" height={120} width={120} />
        </div>
        <h2>Create your own pizza!</h2>
        <LinkContainer to="/pizza-creator">
          <Button>
            <h4>Start creating pizza</h4>
          </Button>
        </LinkContainer>
      </div>
    </div>
  );
}
