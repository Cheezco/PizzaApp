import { Outlet } from "react-router-dom";

export default function Root() {
  return (
    <div className="d-flex justify-content-center w-100 h-100 align-items-center">
      <Outlet />
    </div>
  );
}
