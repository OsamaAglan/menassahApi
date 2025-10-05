using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Menassah;
using Menassah.DAL;
using Menassah.Repo;
using Menassah.Repository;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using VSoftCore;

var builder = WebApplication.CreateBuilder(args);

// ========= JWT Settings =========
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// ========= Add Services =========
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// ========= Swagger =========
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Menassah API", Version = "v1" });

    // Add JWT Bearer token support
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "أدخل التوكن بهذا الشكل: Bearer <token>"
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

// ========= Register Services =========
builder.Services.AddScoped<IBaseDAL, BaseDAL>();
builder.Services.AddScoped<IMainHelper, MainHelper>();
builder.Services.AddScoped<IDashboardRepo, DashboardDAL>();
builder.Services.AddScoped<IUsersRepo, UsersDAL>();
builder.Services.AddScoped<IRolesRepo, RolesDAL>();
builder.Services.AddScoped<IStudentsRepo, StudentsDAL>();
builder.Services.AddScoped<IUserRolesRepo, UserRolesDAL>();
builder.Services.AddScoped<ICitiesRepo, CitiesDAL>();
builder.Services.AddScoped<ITeachersRepo, TeachersDAL>();
builder.Services.AddScoped<ITeacherSubjectsRepo, TeacherSubjectsDAL>();
builder.Services.AddScoped<ITeacherGradesRepo, TeacherGradesDAL>();
builder.Services.AddScoped<ITeacherGroupsRepo, TeacherGroupsDAL>();
builder.Services.AddScoped<IQuestionsRepo, QuestionsDAL>();
builder.Services.AddScoped<ISocialMediaRepo, SocialMediaDAL>();
builder.Services.AddScoped<IUploadsRepo, UploadsDAL>();
builder.Services.AddScoped<ISubjectsRepo, SubjectsDAL>();
builder.Services.AddScoped<IGradesRepo, GradesDAL>();
builder.Services.AddScoped<IStudentGroupsRepo, StudentGroupsDAL>();
builder.Services.AddScoped<IStudentAnswersRepo, StudentAnswersDAL>();
builder.Services.AddScoped<INotificationsRepo , NotificationsDAL>();
builder.Services.AddScoped<ILessonsRepo, LessonsDAL>();

// خدمة توليد JWT Token
builder.Services.AddScoped<ITokenService, TokenService>();

// ========= CORS =========
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// ========= Authentication =========
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
        };
    });

// ========= Configure Web Server Port =========
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(System.Net.IPAddress.Any, 5051);
});

// ========= Build App =========
var app = builder.Build();

// ========= Middleware Pipeline =========
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
