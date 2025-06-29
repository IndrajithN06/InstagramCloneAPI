# Render Deployment Guide for Instagram Clone API

## Prerequisites
- Render account
- GitHub repository with your code
- Cloudinary account (for image uploads)

## Step 1: Choose Your Database

### Option A: Use Render PostgreSQL (Recommended - Easiest)
1. Go to your Render dashboard
2. Click "New" → "PostgreSQL"
3. Choose a name (e.g., `instagram-clone-db`)
4. Select a plan (Free tier available)
5. Choose a region (same as your API)
6. Click "Create Database"
7. Note down the connection details

### Option B: Use Azure SQL Database (If you need SQL Server)
1. Go to [Azure Portal](https://portal.azure.com/)
2. Create new **SQL Database**
3. **Server name**: `instagramclone-server` (unique)
4. **Database name**: `InstagramCloneDB`
5. **Authentication**: SQL authentication
6. **Admin login**: `admin`
7. **Password**: Create strong password
8. **Compute**: Basic (free tier)

## Step 2: Deploy API to Render

1. **Connect GitHub Repository**:
   - Go to Render dashboard
   - Click "New" → "Web Service"
   - Connect your GitHub repository
   - Select the repository

2. **Configure Service**:
   - **Name**: `instagram-clone-api`
   - **Environment**: `Docker`
   - **Region**: Same as your database
   - **Branch**: `main` (or your default branch)
   - **Root Directory**: Leave empty (if code is in root)

3. **Set Environment Variables**:
   Add these in the Render dashboard:

   **For PostgreSQL**:
   ```
   DB_SERVER=your-postgres-host.render.com
   DB_NAME=your-database-name
   DB_USER=your-username
   DB_PASSWORD=your-password
   ```

   **For Azure SQL**:
   ```
   DB_SERVER=instagramclone-server.database.windows.net
   DB_NAME=InstagramCloneDB
   DB_USER=admin
   DB_PASSWORD=YourStrongPassword123!
   ```

   **JWT Configuration**:
   ```
   JWT_KEY=your-super-secret-jwt-key-here-make-it-long-and-random
   JWT_ISSUER=InstagramCloneAPI
   JWT_AUDIENCE=InstagramCloneAPI
   ```

   **Cloudinary Settings**:
   ```
   CLOUDINARY_CLOUD_NAME=your-cloudinary-cloud-name
   CLOUDINARY_API_KEY=your-cloudinary-api-key
   CLOUDINARY_API_SECRET=your-cloudinary-api-secret
   ```

4. **Deploy**:
   - Click "Create Web Service"
   - Render will build and deploy your application

## Step 3: Database Migration

After deployment, you need to run database migrations:

1. **Option A: Using Render Shell**:
   ```bash
   # Connect to your Render service shell
   # Navigate to your app directory
   dotnet ef database update
   ```

2. **Option B: Using Local Connection**:
   ```bash
   # Set connection string to production database
   dotnet ef database update --connection "your-production-connection-string"
   ```

## Step 4: Update Connection String (if using PostgreSQL)

If you switch to PostgreSQL, update your `appsettings.Production.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=${DB_SERVER};Database=${DB_NAME};Username=${DB_USER};Password=${DB_PASSWORD};"
  }
}
```

And add PostgreSQL package to your project:
```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Update `Program.cs`:
```csharp
// Replace SQL Server with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

## Step 5: Verify Deployment

1. Check your API URL: `https://your-app-name.onrender.com`
2. Test Swagger UI: `https://your-app-name.onrender.com/swagger`
3. Test your endpoints

## Troubleshooting

### Common Issues:

1. **Database Connection Failed**:
   - Check environment variables
   - Verify database is running
   - Check firewall settings (for Azure SQL)

2. **Migration Errors**:
   - Ensure database exists
   - Check connection string format
   - Verify user permissions

3. **Build Failures**:
   - Check Dockerfile syntax
   - Verify all dependencies are included
   - Check for missing files

### Environment Variables Reference:

| Variable | Description | Example |
|----------|-------------|---------|
| `DB_SERVER` | Database server hostname | `postgresql-host.render.com` |
| `DB_NAME` | Database name | `instagram_clone_db` |
| `DB_USER` | Database username | `postgres` |
| `DB_PASSWORD` | Database password | `your-password` |
| `JWT_KEY` | JWT secret key | `your-256-bit-secret-key` |
| `CLOUDINARY_CLOUD_NAME` | Cloudinary cloud name | `your-cloud-name` |
| `CLOUDINARY_API_KEY` | Cloudinary API key | `your-api-key` |
| `CLOUDINARY_API_SECRET` | Cloudinary API secret | `your-api-secret` |

## Security Notes

1. **Never commit secrets** to your repository
2. **Use strong JWT keys** (at least 256 bits)
3. **Enable HTTPS** (Render does this automatically)
4. **Set up proper CORS** for your frontend domain
5. **Regularly update dependencies**

## Next Steps

1. Set up your frontend to use the new API URL
2. Configure CORS to allow your frontend domain
3. Set up monitoring and logging
4. Configure custom domain (optional) 