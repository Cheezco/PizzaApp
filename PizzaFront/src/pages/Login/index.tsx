import { useState } from "react";
import { Button } from "react-bootstrap";
import { Card, Form } from "react-bootstrap";
import { login } from "../../lib/api/auth";
import { useNavigate } from "react-router-dom";

export function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showError, setShowError] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async () => {
    setShowError(false);
    if (username === "" || password === "") return;

    const success = await login({ username: username, password: password });
    setShowError(!success);
    navigate("/");
  };

  return (
    <div className="d-flex w-100 h-75 justify-content-center align-items-center">
      <Card className="shadow">
        <Card.Body>
          {showError && <div>Failed to login</div>}
          <div className="py-2">
            DEMO:
            <br />
            username: <b>user</b>
            <br />
            password: <b>!Password123</b>
          </div>
          <Form>
            <Form.Group>
              <Form.Label>Username</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter username"
                onChange={(e: unknown) => {
                  setUsername(e.target.value);
                }}
              />
            </Form.Group>

            <Form.Group>
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                placeholder="Password"
                onChange={(e: unknown) => {
                  setPassword(e.target.value);
                }}
              />
            </Form.Group>

            <div className="pt-2 w-100 d-flex justify-content-end">
              <Button variant="primary" onClick={handleSubmit}>
                Login
              </Button>
            </div>
          </Form>
        </Card.Body>
      </Card>
    </div>
  );
}
