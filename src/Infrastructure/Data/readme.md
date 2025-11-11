add-migration db-initial -Context ApplicationDbContext -Project Infrastructure -StartupProject Api  -o Data\Migrations

update-database -Context ApplicationDbContext