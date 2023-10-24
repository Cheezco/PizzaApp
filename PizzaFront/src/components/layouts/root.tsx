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
import { useJwt } from "react-jwt";
import { clearToken, getToken } from "../../lib/authUtils";
import { JwtToken } from "../../types/authTypes";
import { useEffect, useState } from "react";

export default function Root() {
  return (
    <div className="h-100 w-100 d-flex flex-column">
      <RootNavbar />
      <div className="d-flex w-100 h-100">
        <Outlet />
      </div>
    </div>
  );
}

function RootNavbar() {
  const { decodedToken, isExpired, reEvaluateToken } = useJwt<JwtToken>(
    getToken()
  );
  const [roles, setRoles] = useState<string[]>([]);
  const [loggedIn, setLoggedIn] = useState(false);

  useEffect(() => {
    const interval = setInterval(() => {
      reEvaluateToken(getToken());

      if (!decodedToken || isExpired) {
        setLoggedIn(false);
        setRoles([]);
        return;
      }

      setRoles(
        decodedToken[
          "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        ]
      );

      setLoggedIn(true);
    }, 100);

    return () => clearInterval(interval);
  }, [decodedToken, isExpired, reEvaluateToken]);

  return (
    <Navbar className="position-sticky top-0">
      <Container fluid className="px-5">
        <LinkContainer to="/">
          <Navbar.Brand>PizzaMe</Navbar.Brand>
        </LinkContainer>
        <Nav>
          <LinkContainer to="/pizza-creator">
            <Nav.Link>Create Your Pizza</Nav.Link>
          </LinkContainer>
          {!loggedIn && (
            <>
              <LinkContainer to="/login">
                <Nav.Link>Login</Nav.Link>
              </LinkContainer>
              <LinkContainer to="/register">
                <Nav.Link disabled={true}>Register</Nav.Link>
              </LinkContainer>
            </>
          )}
          {loggedIn && (
            <>
              <LinkContainer to="/orders">
                <Nav.Link>My Orders</Nav.Link>
              </LinkContainer>
              {roles?.includes("Admin") && (
                <Dropdown as={NavItem}>
                  <Dropdown.Toggle as={NavLink}>Admin</Dropdown.Toggle>
                  <Dropdown.Menu>
                    <Dropdown.Item>Orders</Dropdown.Item>
                  </Dropdown.Menu>
                </Dropdown>
              )}
              <Nav.Link
                as="button"
                onClick={() => {
                  clearToken();
                  setLoggedIn(false);
                }}
              >
                Logout
              </Nav.Link>
            </>
          )}
        </Nav>
      </Container>
    </Navbar>
  );
}
