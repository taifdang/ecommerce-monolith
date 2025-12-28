import { NavBar } from "@/shared/components/layout/NavBar";
import { CartHeader } from "../../components/CartHeader";
import s from "./index.module.css";
import clsx from "clsx";
import BasketItem from "../../components/BasketItem";
import { useNavigate } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { fetchBasket } from "../../services/basket-service";
import { useEffect, useMemo, useState } from "react";
import { formatCurrency } from "@/shared/lib/currency";

export function BasketPage() {
  const navigate = useNavigate();
  const [basketItem, setBasketItem] = useState([]);

  const { data: basket } = useQuery({
    queryKey: ["basket"],
    queryFn: () => fetchBasket().then((res) => res.data),
    retry: false,
    refetchOnWindowFocus: false,
    initialData: {
      id: "",
      customerId: "",
      items: [],
      createdAt: new Date().toDateString(),
      lastModified: null,
    },
  });

  const mutationPrice = (item) => {
    let result = 0;
    if (!item) return result;
    for (let i = 0; i < item.length; i++) {
      result += item[i].regularPrice * item[i].quantity;
    }
    return result;
  };

  const totalPrice = useMemo(() => mutationPrice(basket.items));

  useEffect(() => {
    console.log("basket__current:", JSON.stringify(basket));
  }, [basket]);

  const handleCheckout = () => {
    navigate("/checkout");
  };

  return (
    <div>
      <NavBar />
      <div>
        <CartHeader />
        <div className="mx-auto w-[1200px]">
          {/* table section */}
          <div className="flex flex-col pt-[20px]">
            {/* Table header */}
            <div className={s["basket__table-header"]}>
              <div
                className={clsx(s["div-checkbox"], s["table-col--checkbox"])}
              >
                <label htmlFor="">
                  <input type="text" hidden />
                  <div className={s["div-checkbox-wrap-input"]}></div>
                </label>
              </div>
              <div className={clsx(s["table-col"], s["table-col--product"])}>
                Product
              </div>
              <div
                className={clsx(
                  s["table-col"],
                  s["table-col--unit"],
                  "text-center"
                )}
              >
                Unit Price
              </div>
              <div
                className={clsx(
                  s["table-col"],
                  s["table-col--quantity"],
                  "text-center"
                )}
              >
                Quantity
              </div>
              <div
                className={clsx(
                  s["table-col"],
                  s["table-col--total"],
                  "text-center"
                )}
              >
                Total Price
              </div>
              <div
                className={clsx(
                  s["table-col"],
                  s["table-col--actions"],
                  "text-center"
                )}
              >
                Actions
              </div>
            </div>
            {/* Table content */}
            <div className={s["basket__table-content"]}>
              <section className={s["table-content__section"]}>
                {/* Title */}
                <div className={s["table-content__title"]}>
                  <span>Items: 0</span>
                </div>
                {/* CartItem */}
                <div>
                  {basket && basket.items.length === 0 ? (
                    <>
                      <span>null basket</span>
                    </>
                  ) : (
                    <>
                      {basket.items.map((item, index) => (
                        <>
                          <BasketItem key={item.id} item={item} />
                          {index < basket.items.length - 1 && (
                            <div className={s["basket__item-divider"]}></div>
                          )}
                        </>
                      ))}
                    </>
                  )}
                </div>
              </section>
            </div>
          </div>
          {/* basket footer */}
          <section className={s["basket__footer"]}>
            {/* promotion */}
            <div className={s["basket__footer-promotion"]}>
              <img
                style={{ marginRight: "8px" }}
                src="src/assets/images/voucher_icon.svg"
              />
              <div>Platform voucher</div>
              <div className="flex-1"></div>
              <button className={s["basket__footer-promotion-button"]}>
                Select or enter code
              </button>
            </div>
            {/*  */}
            <div className={s["basket__footer-divider"]}></div>
            {/* total */}
            <div className={s["basket__footer-total"]}>
              {/* selection */}
              <div
                className={clsx(s["div-checkbox"], s["table-col--checkbox"])}
              >
                <label htmlFor="">
                  <input type="text" hidden />
                  <div className={s["div-checkbox-wrap-input"]}></div>
                </label>
              </div>
              <button className={s["basket__footer--selected"]}>
                Select All (0)
              </button>
              <button className={s["basket__footer--unselected"]}>
                Delete
              </button>
              {/*  */}
              <div></div>
              {/* text */}
              <div className="flex-1"></div>
              <div className="flex flex-col">
                <div className="flex items-center flex-end">
                  <div
                    className={clsx(
                      "flex items-center",
                      s["basket__footer-total-title"]
                    )}
                  >
                    Total ({basket.items.length} item):
                  </div>
                  <div className={s["basket__footer-total-subtitle"]}>{formatCurrency(totalPrice)}</div>
                </div>
              </div>
              {/* button */}
              <div>
                <button
                  onClick={() => handleCheckout()}
                  className={s["basket__footer-button"]}
                >
                  <span className={s["basket__footer-button-title"]}>
                    Checkout
                  </span>
                </button>
              </div>
            </div>
          </section>
        </div>
      </div>
    </div>
  );
}
