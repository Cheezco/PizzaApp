import { createBrowserRouter } from "react-router-dom";
import Root from "./components/layouts/root";
import ErrorPage from "./pages/Error";
import { Login } from "./pages/Login";
import { Register } from "./pages/Register";
import { Orders } from "./pages/Orders";
import { PizzaCreator } from "./pages/PizzaCreator";
import { Home } from "./pages/Home";
import AdminOrders from "./pages/Admin/Orders";

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
        {
          path: "login",
          element: <Login />,
        },
        {
          path: "register",
          element: <Register />,
        },
      ],
    },
    {
      path: "/admin",
      element: <Root />,
      errorElement: <ErrorPage />,
      children: [
        {
          path: "orders",
          element: <AdminOrders />,
        },
      ],
    },
  ]);
}
