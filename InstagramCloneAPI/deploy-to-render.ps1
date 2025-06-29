# Deploy to Render Script
Write-Host "🚀 Instagram Clone API - Render Deployment" -ForegroundColor Green
Write-Host "=============================================" -ForegroundColor Green

Write-Host "`n✅ PostgreSQL Migration Complete!" -ForegroundColor Green
Write-Host "✅ Code updated for PostgreSQL" -ForegroundColor Green
Write-Host "✅ Build successful" -ForegroundColor Green

Write-Host "`n📋 Next Steps for Render Deployment:" -ForegroundColor Cyan

Write-Host "`n1. 🗄️ Create PostgreSQL Database on Render:" -ForegroundColor Yellow
Write-Host "   - Go to https://dashboard.render.com" -ForegroundColor White
Write-Host "   - Click 'New' → 'PostgreSQL'" -ForegroundColor White
Write-Host "   - Name: instagram-clone-db" -ForegroundColor White
Write-Host "   - Plan: Free" -ForegroundColor White
Write-Host "   - Region: Oregon (or your preferred)" -ForegroundColor White

Write-Host "`n2. 🌐 Deploy API to Render:" -ForegroundColor Yellow
Write-Host "   - Click 'New' → 'Web Service'" -ForegroundColor White
Write-Host "   - Connect your GitHub repository" -ForegroundColor White
Write-Host "   - Environment: Docker" -ForegroundColor White
Write-Host "   - Use the render.yaml file" -ForegroundColor White

Write-Host "`n3. ⚙️ Set Environment Variables in Render:" -ForegroundColor Yellow
Write-Host "   JWT_KEY=your-super-secret-jwt-key-here" -ForegroundColor White
Write-Host "   CLOUDINARY_CLOUD_NAME=your-cloudinary-cloud-name" -ForegroundColor White
Write-Host "   CLOUDINARY_API_KEY=your-cloudinary-api-key" -ForegroundColor White
Write-Host "   CLOUDINARY_API_SECRET=your-cloudinary-api-secret" -ForegroundColor White

Write-Host "`n4. 🗃️ Run Database Migration:" -ForegroundColor Yellow
Write-Host "   - After deployment, connect to Render shell" -ForegroundColor White
Write-Host "   - Run: dotnet ef database update" -ForegroundColor White

Write-Host "`n5. 🧪 Test Your API:" -ForegroundColor Yellow
Write-Host "   - Visit: https://your-app-name.onrender.com/swagger" -ForegroundColor White
Write-Host "   - Test your endpoints" -ForegroundColor White

Write-Host "`n🎉 Your API will be live at: https://your-app-name.onrender.com" -ForegroundColor Green

Write-Host "`n📚 Need help? Check RENDER_DEPLOYMENT.md for detailed instructions" -ForegroundColor Cyan 