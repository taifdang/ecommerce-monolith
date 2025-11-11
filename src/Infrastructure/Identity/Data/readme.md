add-migration identity-initial -Context AppIdentityDbContext -Project Infrastructure -StartupProject Api  -o Identity\Data\Migrations

update-database -Context AppIdentityDbContext