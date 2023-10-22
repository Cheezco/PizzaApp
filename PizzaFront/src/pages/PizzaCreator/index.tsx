import { useState } from "react";
import { Step, Stepper } from "react-form-stepper";
import SizeTab from "./SizeTab";
import ToppingTab from "./ToppingTab";
import OrderTab from "./OrderTab";
import { Card } from "react-bootstrap";

export function PizzaCreator() {
  const [activeStep, setActiveStep] = useState(0);

  const handleStepChange = (step: number) => {
    if (step < 0 || step > 2) return;

    setActiveStep(step);
  };

  return (
    <div className="w-100 h-75 d-flex justify-content-center align-items-center py-4">
      <Card className="w-50 shadow">
        <Card.Body className="d-flex w-100 h-100 flex-column">
          <Stepper
            activeStep={activeStep}
            styleConfig={{
              activeBgColor: "#2D4B8B",
              activeTextColor: "#ffffff",
              completedBgColor: "#0a111f",
              completedTextColor: "#ffffff",
              inactiveBgColor: "#e0e0e0",
              inactiveTextColor: "#ffffff",
              size: "2em",
              circleFontSize: "1rem",
              labelFontSize: "0.875rem",
              borderRadius: "50%",
              fontWeight: 500,
            }}
          >
            <Step
              onClick={() => {
                setActiveStep(0);
              }}
              label="Pizza size"
            ></Step>
            <Step
              onClick={() => {
                setActiveStep(1);
              }}
              label="Toppings"
            ></Step>
            <Step
              onClick={() => {
                console.log(2);
              }}
              label="Order"
            ></Step>
          </Stepper>
          {activeStep == 0 ? (
            <SizeTab currentStep={0} setActiveStep={handleStepChange} />
          ) : activeStep == 1 ? (
            <ToppingTab currentStep={1} setActiveStep={handleStepChange} />
          ) : (
            <OrderTab currentStep={2} setActiveStep={handleStepChange} />
          )}
        </Card.Body>
      </Card>
    </div>
  );
}
