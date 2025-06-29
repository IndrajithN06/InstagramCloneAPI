# Quick Migration Guide - When Free Tier Expires

## ⚠️ What Happens After 1 Month
- Your Render PostgreSQL database gets **DELETED**
- All data is **LOST**
- Your API will **STOP WORKING**

## 🚀 Quick Recovery Steps

### Step 1: Create New Database
1. Go to Render Dashboard
2. Click "New" → "PostgreSQL"
3. Name: `instagram-clone-db-v2` (or any name)
4. Plan: Free (or upgrade to paid)
5. Region: Same as before

### Step 2: Update Environment Variables
In your Render API service, update these variables:
```
DB_SERVER=new-database-host.render.com
DB_NAME=new-database-name
DB_USER=new-username
DB_PASSWORD=new-password
```

### Step 3: Run Migration
Connect to your Render API shell and run:
```bash
dotnet ef database update
```

### Step 4: Test API
Visit: `https://your-app-name.onrender.com/swagger`

## 💡 Better Solutions

### Option A: Upgrade to Paid ($7/month)
- Go to database dashboard
- Click "Upgrade Plan"
- Choose "Starter" plan
- Data persists forever

### Option B: Use External Database
- **Railway**: Free with $5 credit
- **Supabase**: Free 500MB
- **Neon**: Free 3GB
- **PlanetScale**: Free 1GB

## 🔄 If You Have Important Data
Before the database gets deleted:
1. Export your data (if any)
2. Create new database
3. Run migrations
4. Re-import your data

## 📞 Need Help?
- Check `RENDER_DEPLOYMENT.md` for full guide
- Run `.\backup-database.ps1` for more info 