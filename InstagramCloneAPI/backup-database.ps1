# Database Backup and Restore Script for Render Free Tier
Write-Host "🗄️ Database Backup Strategy for Render Free Tier" -ForegroundColor Green
Write-Host "=================================================" -ForegroundColor Green

Write-Host "`n⚠️  WARNING: Render Free Tier PostgreSQL gets deleted after 1 month!" -ForegroundColor Red
Write-Host "Your data will be lost unless you upgrade or use external database." -ForegroundColor Red

Write-Host "`n📋 Solutions:" -ForegroundColor Cyan

Write-Host "`n1. 💰 Upgrade to Paid Plan ($7/month):" -ForegroundColor Yellow
Write-Host "   - Go to your Render database dashboard" -ForegroundColor White
Write-Host "   - Click 'Upgrade Plan'" -ForegroundColor White
Write-Host "   - Choose 'Starter' plan" -ForegroundColor White
Write-Host "   - Your data will be preserved" -ForegroundColor White

Write-Host "`n2. 🌐 Use External Free Database:" -ForegroundColor Yellow
Write-Host "   - Railway PostgreSQL (Free with $5 credit)" -ForegroundColor White
Write-Host "   - Supabase (Free 500MB)" -ForegroundColor White
Write-Host "   - Neon (Free 3GB)" -ForegroundColor White
Write-Host "   - PlanetScale (Free 1GB)" -ForegroundColor White

Write-Host "`n3. 🔄 Manual Backup Process:" -ForegroundColor Yellow
Write-Host "   Before database deletion:" -ForegroundColor White
Write-Host "   - Export your data (if any)" -ForegroundColor White
Write-Host "   - Create new database" -ForegroundColor White
Write-Host "   - Run migrations: dotnet ef database update" -ForegroundColor White
Write-Host "   - Re-import your data" -ForegroundColor White

Write-Host "`n4. 🚀 Quick Migration Commands:" -ForegroundColor Yellow
Write-Host "   # After creating new database:" -ForegroundColor White
Write-Host "   dotnet ef database update" -ForegroundColor White
Write-Host "   # Update environment variables in Render" -ForegroundColor White

Write-Host "`n💡 RECOMMENDATION: Upgrade to paid plan for production apps!" -ForegroundColor Green
Write-Host "   $7/month is worth it for data persistence and reliability." -ForegroundColor White 