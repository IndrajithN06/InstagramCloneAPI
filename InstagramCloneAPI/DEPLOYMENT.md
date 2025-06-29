# Instagram Clone API - Deployment Guide

## Deploy to Railway (Recommended - No Credit Card Required)

### Prerequisites
1. GitHub account (IndrajithN06)
2. Railway account (free at https://railway.app) - No credit card required
3. Railway SQL Server database (free tier - $5 credit monthly)

### Step 1: Set Up Railway SQL Server Database
1. Go to [Railway Dashboard](https://railway.app)
2. Click **"New Project"**
3. Click **"Provision SQL Server"**
4. **Database name**: `InstagramCloneDB`
5. **Username**: `admin`
6. **Password**: Auto-generated (copy and save this!)
7. **Plan**: Free (uses your $5 monthly credit)

### Step 2: Get Connection Details
After creation, go to your database → **Connect** tab:
- **Host**: `your-db-host.railway.app`
- **Database**: `InstagramCloneDB`
- **User**: `admin`
- **Password**: Your auto-generated password
- **Port**: `1433`

### Step 3: Deploy Your API to Railway
1. In the same Railway project, click **"New Service"**
2. Click **"Deploy from GitHub repo"**
3. Select your repository
4. Railway will automatically detect the Dockerfile and deploy

### Step 4: Configure Environment Variables
In Railway dashboard, add these environment variables:

#### Database (Railway SQL Server)
```
DB_SERVER=your-db-host.railway.app
DB_NAME=InstagramCloneDB
DB_USER=admin
DB_PASSWORD=your_auto_generated_password
```

#### JWT Configuration
```
JWT_KEY=YourSuperSecretJWTKey123!@#$%^&*()_+-=[]{}|;:,.<>?
JWT_ISSUER=InstagramCloneAPI
JWT_AUDIENCE=InstagramCloneAPI
```

#### Cloudinary (Your new keys)
```
CLOUDINARY_CLOUD_NAME=your_new_cloud_name
CLOUDINARY_API_KEY=your_new_api_key
CLOUDINARY_API_SECRET=your_new_api_secret
```

### Step 5: Run Database Migrations
After deployment, your API will automatically create the database schema using Entity Framework migrations.

### Step 6: Test Your API
Your API will be available at: `https://your-app-name.railway.app`
- **Swagger UI**: `https://your-app-name.railway.app/swagger`
- **Health Check**: `https://your-app-name.railway.app/health`

### Troubleshooting
- **Database Connection Issues**: Check if database and API are in same project
- **Migration Errors**: Ensure database user has proper permissions
- **Build Errors**: Check Dockerfile and .NET version compatibility

### Security Notes
- ✅ Never commit sensitive data to GitHub
- ✅ Use environment variables for all secrets
- ✅ Regenerate Cloudinary keys after security fix
- ✅ Use strong passwords for database

### Alternative: Render PostgreSQL (If Railway doesn't work)
If you prefer Render, they offer PostgreSQL for free:
1. Go to [Render Dashboard](https://dashboard.render.com/)
2. Create **PostgreSQL** database
3. Use the same environment variables but with PostgreSQL connection string

## Deploy to Render (Recommended)

### Prerequisites
1. GitHub account (IndrajithN06)
2. Render account (free at https://render.com)
3. Azure SQL Database (free tier)

### Step 1: Set Up Azure SQL Database
1. Go to [Azure Portal](https://portal.azure.com/)
2. Create new **SQL Database**
3. **Server name**: `instagramclone-server` (unique)
4. **Database name**: `InstagramCloneDB`
5. **Authentication**: SQL authentication
6. **Admin login**: `admin`
7. **Password**: Create strong password
8. **Compute**: Basic (free tier)

### Step 2: Get Connection Details
1. Go to your database → **Connection strings** → **ADO.NET**
2. Copy the connection string
3. Extract these values:
   - **Server**: `instagramclone-server.database.windows.net`
   - **Database**: `InstagramCloneDB`
   - **User**: `admin`
   - **Password**: Your password

### Step 3: Deploy to Render
1. Go to [Render Dashboard](https://dashboard.render.com/)
2. Click **New +** → **Web Service**
3. Connect GitHub and select your repository
4. **Environment**: Docker
5. **Root Directory**: Leave empty (root)
6. **Build Command**: Leave empty (uses Dockerfile)
7. **Start Command**: Leave empty (uses Dockerfile)

### Step 4: Configure Environment Variables
In Render dashboard, add these environment variables:

#### Database (Azure SQL)
```
DB_SERVER=instagramclone-server.database.windows.net
DB_NAME=InstagramCloneDB
DB_USER=admin
DB_PASSWORD=YourStrongPassword123!
```

#### JWT Configuration
```
JWT_KEY=YourSuperSecretJWTKey123!@#$%^&*()_+-=[]{}|;:,.<>?
JWT_ISSUER=InstagramCloneAPI
JWT_AUDIENCE=InstagramCloneAPI
```

#### Cloudinary (Your new keys)
```
CLOUDINARY_CLOUD_NAME=your_new_cloud_name
CLOUDINARY_API_KEY=your_new_api_key
CLOUDINARY_API_SECRET=your_new_api_secret
```

### Step 5: Run Database Migrations
After deployment, your API will automatically create the database schema using Entity Framework migrations.

### Step 6: Test Your API
Your API will be available at: `https://your-app-name.onrender.com`
- **Swagger UI**: `https://your-app-name.onrender.com/swagger`
- **Health Check**: `https://your-app-name.onrender.com/health`

### Troubleshooting
- **Database Connection Issues**: Check firewall settings in Azure SQL
- **Migration Errors**: Ensure database user has proper permissions
- **Build Errors**: Check Dockerfile and .NET version compatibility

### Security Notes
- ✅ Never commit sensitive data to GitHub
- ✅ Use environment variables for all secrets
- ✅ Regenerate Cloudinary keys after security fix
- ✅ Use strong passwords for database

### Local Development Setup
To run locally with PostgreSQL:
1. Install PostgreSQL locally
2. Create database: `InstagramCloneDB`
3. Update `appsettings.Development.json` with your local PostgreSQL credentials
4. Run: `dotnet ef database update`

## Deploy to Railway (Recommended)

### Prerequisites
1. GitHub account (IndrajithN06)
2. Railway account (free at https://railway.app)
3. Database (PostgreSQL recommended)

### Step 1: Prepare Your Repository
1. Push your code to GitHub
2. ✅ CORS is already configured for your GitHub Pages URL: `https://indrajithn06.github.io`
3. Your repository should be ready for deployment

### Step 2: Deploy to Railway
1. Go to [Railway.app](https://railway.app) and sign up
2. Click "New Project" → "Deploy from GitHub repo"
3. Select your repository: `IndrajithN06/InstagramCloneAPI` (or your repo name)
4. Railway will automatically detect the Dockerfile and deploy

### Step 3: Configure Environment Variables
In Railway dashboard, add these environment variables:

#### Database (PostgreSQL)
```
DB_SERVER=your-postgres-server.railway.app
DB_NAME=your-database-name
DB_USER=your-database-user
DB_PASSWORD=your-database-password
```

#### JWT Configuration
```
JWT_KEY=your-super-secret-jwt-key-here-make-it-long-and-random
JWT_ISSUER=InstagramCloneAPI
JWT_AUDIENCE=InstagramCloneAPI
```

#### Cloudinary (Keep your existing values)
```
CLOUDINARY_CLOUD_NAME=dwiwkndu2
CLOUDINARY_API_KEY=322978763368889
CLOUDINARY_API_SECRET=eO9aj3QBIJs8ZhKKtMMNIl54mwE
```

### Step 4: Add Database
1. In Railway dashboard, click "New" → "Database" → "PostgreSQL"
2. Copy the connection details to your environment variables
3. Run migrations: `dotnet ef database update`

### Step 5: Update Frontend
Update your Angular app's API base URL to your Railway domain:
```typescript
// environment.ts
export const environment = {
  production: false,
  apiUrl: 'https://your-app-name.railway.app'
};

// environment.prod.ts
export const environment = {
  production: true,
  apiUrl: 'https://your-app-name.railway.app'
};
```

## Alternative: Deploy to Render

1. Go to [Render.com](https://render.com)
2. Create new Web Service
3. Connect your GitHub repo
4. Use the same Dockerfile
5. Configure environment variables as above

## Alternative: Deploy to Fly.io

1. Install Fly CLI: `curl -L https://fly.io/install.sh | sh`
2. Run: `fly launch`
3. Follow the prompts
4. Deploy: `fly deploy`

## Health Check
Your API will be available at: `https://your-app-name.railway.app/swagger`

## Important Notes
- ✅ CORS is configured for your GitHub Pages URL: `https://indrajithn06.github.io`
- Keep your JWT key secret and secure
- Consider using a production database service
- Monitor your Railway usage (free tier has limits)
- Your frontend at `https://indrajithn06.github.io` will be able to communicate with the API 