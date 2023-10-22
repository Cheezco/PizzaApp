import { Button, InputGroup, Tab, Tabs } from "react-bootstrap";

export default function ToppingTab({
  currentStep,
  setActiveStep,
}: {
  currentStep: number;
  setActiveStep: (step: number) => void;
}) {
  const toppingCategories = getToppings();

  return (
    <div className="d-flex flex-column gap-3">
      <div className="h-100 w-100 d-flex justify-content-center">
        <h3>Choose pizza toppings</h3>
      </div>
      <Tabs defaultActiveKey={toppingCategories[0].id}>
        {toppingCategories.map((category) => {
          return (
            <Tab eventKey={category.id} title={category.name}>
              <div className="d-flex gap-2 flex-wrap">
                {category.toppings.map((topping) => {
                  return (
                    <div className="d-flex flex-column border p-2 align-items-center rounded shadow-sm">
                      <div>{topping.name}</div>
                      <div>{topping.price}</div>
                      <InputGroup>
                        <Button>-</Button>
                        <InputGroup.Text>0</InputGroup.Text>
                        <Button>+</Button>
                      </InputGroup>
                    </div>
                  );
                })}
              </div>
            </Tab>
          );
        })}
      </Tabs>
      <div className="d-flex gap-2 w-100 justify-content-end">
        <Button
          variant="secondary"
          onClick={() => setActiveStep(currentStep - 1)}
        >
          Change size
        </Button>
        <Button onClick={() => setActiveStep(currentStep + 1)}>Order</Button>
      </div>
    </div>
  );
}

function getToppings() {
  return [
    {
      id: 1,
      name: "Cheese",
      toppings: [
        {
          id: 1,
          name: "Cheddar",
          limit: 3,
          price: 1,
        },
        {
          id: 2,
          name: "Feta",
          limit: 2,
          price: 1,
        },
      ],
    },
    {
      id: 2,
      name: "Meat",
      toppings: [
        {
          id: 3,
          name: "Bacon",
          limit: 3,
          price: 1,
        },
        {
          id: 4,
          name: "Beef",
          limit: 2,
          price: 1,
        },
      ],
    },
  ];
}
