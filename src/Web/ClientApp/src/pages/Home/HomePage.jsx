import { useState, useEffect } from "react";
import { ProductCard } from "./components/ProductCard";
import { Pagination } from "../../shared/components/Pagination";

export default function HomePage() {
  const [page, setPage] = useState(1);
  const totalPage = 1;

  useEffect(() => {
    fetch("api/v1/catalog/products/get-available?PageSize=10&PageIndex=0")
      .then((res) => res.json())
      .then((data) => console.log(data))
      .catch((err) => console.error(err));
    
  }, []);

  return (
    <div>
      <div className="layout-main">
        <div className="suggest container-wrapper">
          <h1 className="suggest__label">Daily Recover</h1>
          <hr className="suggest__divider" />
        </div>
        <div className="container-wrapper h-full mx-auto layout-section">
          <div className="flex flex-wrap mx-auto">
            {Array.from({ length: 18 }).map((_, i) => (
              <ProductCard key={i} />
            ))}
          </div>
        </div>
        {/* -------- PAGINATION -------- */}
        <Pagination
          currentPage={page}
          totalPage={totalPage}
          onChange={setPage}
        />
      </div>
    </div>
  );
}
