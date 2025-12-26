import { ProductList } from "../features/catalog/components/ProductList";

export default function HomePage() {
  return (
    <div>
      <div className="layout-main">
        <div className="suggest container-wrapper">
          <h1 className="suggest__label">DAILY DISCOVER</h1>
          <hr className="suggest__divider" />
        </div>
        {/*  Main product list */}
        <ProductList />
      </div>
    </div>
  );
}
