import clsx from "clsx";
export default function PaymentProvider({ items, value, onChange }) {
  return (
    <div style={{ display: "contents" }}>
      <div className="checkout-payment-method-main">
        <div className="checkout-payment-method-view__current">
          <div className="checkout-payment-method-view__current-title">
            Payment Method
          </div>
          <div>
            <div
              role="radiogroup"
              className="checkout-payment-setting__payment-methods-tabs"
            >
              {items.map((p) => {
                const __selected = value === p.id;
                return (
                  <span key={p.id}>
                    <button
                      className={clsx(
                        "product-variation selection__box",
                        __selected
                          ? "selection__box--selected"
                          : "selection__box--unselected"
                      )}
                      aria-checked={__selected}
                      role="radio"
                      onClick={() => onChange(p.id)}
                    >
                      {p.label}
                      {__selected && (
                        <div className="selection__box-icon"></div>
                      )}
                    </button>
                  </span>
                );
              })}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
