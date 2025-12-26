export function PriceBox({ price, regularPrice, discount }) {
  return (
    <div style={{ marginTop: "10px" }}>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          padding: "15px 20px",
          backgroundColor: "rgb(248, 249, 250)",
        }}
      >
        <section
          style={{
            height: "36px",
            display: "flex",
            alignItems: "center",
          }}
        >
          <div style={{ fontSize: "30px", fontWeight: "500" }}>{price}</div>
          {/*  */}
          <div
            style={{
              marginLeft: "10px",
              textDecoration: "line-through",
              color: "var(--color-gray-600)",
            }}
          >
            {regularPrice}
          </div>
          {/*  */}
          {discount && (
            <>
              <div
                style={{
                  marginLeft: "10px",
                  fontSize: "12px",
                  padding: "2px 4px",
                  backgroundColor: "black",
                  color: "white",
                }}
              >
                -33%
              </div>
            </>
          )}
        </section>
      </div>
    </div>
  );
}
