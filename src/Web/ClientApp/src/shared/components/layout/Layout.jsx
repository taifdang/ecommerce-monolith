import { Outlet } from "react-router-dom";
import { Header } from "./Header";
import { Footer } from "./Footer";
import { NavBar } from "./NavBar";

export default function Layout() {
  return (
    <div id="main">
      <div className="flex flex-col relative">
        <header>
          <NavBar />
          <Header />
        </header>
        <main style={{ marginTop: "120px" }}>
          <Outlet />
        </main>
      </div>
    </div>
  );
}
