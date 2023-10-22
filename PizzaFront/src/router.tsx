import { createBrowserRouter } from "react-router-dom";
import Root from "./components/layouts/root";
import ErrorPage from "./pages/Error";
import { Login } from "./pages/Login";
import { Register } from "./pages/Register";
import { Orders } from "./pages/Orders";
import { PizzaCreator } from "./pages/PizzaCreator";
import { Home } from "./pages/Home";

export default function createRouter() {
  return createBrowserRouter([
    {
      path: "/",
      element: <Root />,
      errorElement: <ErrorPage />,
      children: [
        {
          index: true,
          element: <Home />,
        },
        {
          path: "orders",
          element: <Orders />,
        },
        {
          path: "pizza-creator",
          element: <PizzaCreator />,
        },
      ],
    },
    {
      path: "/register",
      element: <Register />,
      errorElement: <ErrorPage />,
    },
    {
      path: "/login",
      element: <Login />,
      errorElement: <ErrorPage />,
    },
  ]);
}
