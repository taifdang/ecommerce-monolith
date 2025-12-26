export function DisplayName({ name }) {
  return (
    <div className="line-clamp-2">
      <h1
        style={{
          fontSize: "20px",
          lineHeight: "36px",
          fontWeight: "500",
          fontFamily: "Arial",
          maxWidth: "665px",
          overflowWrap: "break-word",
        }}
      >
        {name}
      </h1>
    </div>
  );
}
