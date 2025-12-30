import { DisplayName } from "./DisplayName";
import { PriceBox } from "./PriceBox";

export function Info({ price, name, isPriceLoading }) {
  return (
    <div>
      <DisplayName name={name} />
      <PriceBox price={price} isPriceLoading={isPriceLoading} />
    </div>
  );
}
