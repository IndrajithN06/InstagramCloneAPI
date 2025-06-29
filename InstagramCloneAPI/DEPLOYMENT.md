# Instagram Clone API - Deployment Guide

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