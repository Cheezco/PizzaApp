import {
  Container,
  Dropdown,
  Nav,
  NavItem,
  NavLink,
  Navbar,
} from "react-bootstrap";
import { Outlet } from "react-router-dom";
import { LinkContainer } from "react-router-bootstrap";

export default function Root() {
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
      <div className="d-flex w-100 h-100">
        <Outlet />
      </div>
    </div>
  );
}
