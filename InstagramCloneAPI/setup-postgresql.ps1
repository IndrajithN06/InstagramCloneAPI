# Setup PostgreSQL for Render Deployment
Write-Host "Setting up PostgreSQL for Render deployment..." -ForegroundColor Green

# Add PostgreSQL package
Write-Host "Adding PostgreSQL Entity Framework package..." -ForegroundColor Yellow
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

Write-Host "PostgreSQL package added successfully!" -ForegroundColor Green

Write-Host "`nNext steps:" -ForegroundColor Cyan
Write-Host "1. Update Program.cs to use PostgreSQL instead of SQL Server" -ForegroundColor White
Write-Host "2. Update appsettings.Production.json with PostgreSQL connection string" -ForegroundColor White
Write-Host "3. Deploy to Render with the new configuration" -ForegroundColor White
Write-Host "4. Run database migrations on Render" -ForegroundColor White

Write-Host "`nCode changes needed:" -ForegroundColor Yellow
Write-Host "In Program.cs, replace:" -ForegroundColor White
Write-Host "options.UseSqlServer(...) with options.UseNpgsql(...)" -ForegroundColor White

Write-Host "`nIn appsettings.Production.json, update connection string to:" -ForegroundColor White
Write-Host "Host=${DB_SERVER};Database=${DB_NAME};Username=${DB_USER};Password=${DB_PASSWORD};" -ForegroundColor White 