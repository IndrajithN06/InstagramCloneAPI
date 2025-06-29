using InstagramCloneAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Retrieve JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

// Configure authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };

    // 🔥 Add this block for SignalR support
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chathub"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// Configure authorization services
builder.Services.AddAuthorization();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200", 
                "https://localhost:4200",
                "https://indrajithn06.github.io", // Your actual GitHub Pages URL
                "https://*.github.io" // Allow all GitHub Pages subdomains
              )
              .AllowAnyHeader()   // Allows any header.
              .AllowAnyMethod()  // Allows any HTTP method.
              .AllowCredentials();  // For Signal R
    });
});

// Add controllers
builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddSignalR();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));



builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();




// Configure Entity Framework Core with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    try
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseNpgsql(connectionString);
    }
    catch (Exception ex)
    {
        // Log the error but don't crash the application
        Console.WriteLine($"Database configuration error: {ex.Message}");
        // Use in-memory database as fallback
        options.UseInMemoryDatabase("FallbackDatabase");
    }
});

// Configure Swagger with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http, // Correct type for HTTP authentication schemes
        Scheme = "bearer" // Must be lowercase
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});


var app = builder.Build();


// Configure the HTTP request pipeline
// Always enable Swagger (for testing/demo; remove for real production)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection(); // Re-enabled for HTTPS support

// Enable CORS middleware before authentication and authorization
app.UseCors("AllowAll");  // Apply CORS policy
// Map SignalR hub
app.MapHub<ChatHub>("/chatHub");
// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(); // Serve files from wwwroot

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads" // Make the files accessible at /uploads/{filename}
});

app.MapControllers();

app.Run();
