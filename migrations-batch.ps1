# Define the migration name
$MigrationName = "InitialMigration"

# List of DbContexts
$DbContexts = @("ProductDBContext", "TransactionDBContext", "CategoryDBContext", "LocationDBContext")

# Loop through each DbContext and create a migration
foreach ($DbContext in $DbContexts) {
    # Create the migration
    Add-Migration "$MigrationName-$DbContext" -Context $DbContext
}

# Loop through each DbContext and apply the migration
foreach ($DbContext in $DbContexts) {
    # Apply the migration
    Update-Database -Context $DbContext
}