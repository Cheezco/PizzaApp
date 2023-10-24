import {
  Container,
  Dropdown,
  Nav,
  NavItem,
  NavLink,
  Navbar,
} from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { useRouteError, ErrorResponse } from "react-router-dom";
import "./style.css";

export default function ErrorPage() {
  const error = useRouteError() as ErrorResponse;

  return (
    <div className="h-100 w-100 d-flex flex-column">
      <Navbar className="position-sticky top-0">
        <Container fluid className="px-5">
          <LinkContainer to="/">
            <Navbar.Brand>PizzaMe</Navbar.Brand>
          </LinkContainer>
          <Nav>
            <LinkContainer to="/pizza-creator">
              <Nav.Link>Create your pizza</Nav.Link>
            </LinkContainer>
            <LinkContainer to="/orders">
              <Nav.Link>Orders</Nav.Link>
            </LinkContainer>
            <LinkContainer to="/login">
              <Nav.Link>Login</Nav.Link>
            </LinkContainer>
            <LinkContainer to="/register">
              <Nav.Link>Register</Nav.Link>
            </LinkContainer>
            <Dropdown as={NavItem}>
              <Dropdown.Toggle as={NavLink}>Admin</Dropdown.Toggle>
              <Dropdown.Menu>
                <Dropdown.Item>Orders</Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
            <Nav.Link as="div">Logout</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
      <div className="d-flex w-100 h-100 justify-content-center align-items-center flex-column">
        <h1>Oops!</h1>
        <p>Sorry, an unexpected error has occured</p>
        <img
          src="/images/error-doggo.jpg"
          className="error-image rounded border shadow"
        />
        <p>
          <i>{error.statusText}</i>
        </p>
      </div>
    </div>
  );
}
