import { DisplayName } from "./DisplayName";
import { PriceBox } from "./PriceBox";

export function Info({ price, name }) {
  return (
    <div>
      <DisplayName name={name} />
      <PriceBox price={price}/>
    </div>
  );
}
