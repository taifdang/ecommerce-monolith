add-migration Initial_ShopDB -Context ApplicationDbContext -Project Persistence -StartupProject Api  -o Migrations

update-database -Context ApplicationDbContext

add-migration add-payment-in-order-tbl -Context ApplicationDbContext -Project Persistence -StartupProject Api  -o Migrations
