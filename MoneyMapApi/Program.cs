using Menassah;
using Menassah.DAL;
using Menassah.Repo;
using Menassah.Repository;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using System.Text.Json;
using VSoftCore;


var builder = WebApplication.CreateBuilder(args);

// Add controllers and JSON settings
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Menassah API", Version = "v1" });

    // تعريف سكيم الأمن
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "أدخل التوكن بهذا الشكل: Bearer <token>"
    });

    // ربط كل طلب بهذا السكيم الأمني
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

// Register DbContexts

// Register custom services
builder.Services.AddScoped<IBaseDAL, BaseDAL>();
builder.Services.AddScoped<IMainHelper, MainHelper>();
builder.Services.AddScoped<IDashboardRepo, DashboardDAL>();
builder.Services.AddScoped<IUsersRepo, UsersDAL>();
builder.Services.AddScoped<IRolesRepo, RolesDAL>();
builder.Services.AddScoped<IStudentsRepo, StudentsDAL>();
builder.Services.AddScoped<IUserRolesRepo, UserRolesDAL>();



// CORS policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
                    .AllowAnyOrigin()
                    //.WithOrigins("http://localhost:3000", "http://localhost:5174")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Configure Kestrel port
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(System.Net.IPAddress.Any, 5051);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("YourSuperSecretKeyHere"))
        };
    });


var app = builder.Build();

// Development settings
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();
app.Run();
