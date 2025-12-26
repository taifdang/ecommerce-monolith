import s from "./index.module.css";

export function QuantitySelector({ quantity, available, onChange}) {
  let quantityStatus;
  if (!available) quantityStatus = "INSTOCK" // null: test
  if(available === 0) quantityStatus = "OUT OF STOCK"
  if(available) quantityStatus = `${available} pieces available`

  return (
    <section className={s["quantity-selector__section"]}>
      <h2 className={s["quantity-selector__title"]}>Quantity</h2>
      <div style={{ display: "flex", alignItems: "center" }}>
        <div style={{ marginRight: "15px" }}>
          <div className={s["quantity-selector__button-wrapper"]}>
            <button
              aria-label="Decrease"
              className={s["quantity-selector__button"]}
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="24"
                height="24"
                viewBox="0 0 24 24"
              >
                <path
                  fill="currentColor"
                  d="M18 12.998H6a1 1 0 0 1 0-2h12a1 1 0 0 1 0 2"
                />
              </svg>
            </button>
            <input
              aria-label="search-input"
              type="text"
              value="1"
              className={s["quantity-selector__input"]}
            />
            <button
              aria-label="Increase"
              className={s["quantity-selector__button"]}
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="24"
                height="24"
                viewBox="0 0 24 24"
              >
                <path
                  fill="currentColor"
                  d="M19 12.998h-6v6h-2v-6H5v-2h6v-6h2v6h6z"
                />
              </svg>
            </button>
          </div>
        </div>
        <div className={s["quantity-selector__status"]}>{quantityStatus}</div>
      </div>
    </section>
  );
}
