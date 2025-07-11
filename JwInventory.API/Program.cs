using JwInventory.Infrastructure.Data;
using JwInventory.Infrastructure.Repositories;
using JwInventory.Infrastructure.Repositories.Interfaces;
using JwInventory.Infrastructure.Services;
using JwInventory.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JwInventory.Domain.Interfaces.Services;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "JW Inventory API",
        Version = "v1",
        Description = "API para gerenciamento de inventário, autenticação e usuários.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Seu Nome",
            Email = "seu@email.com",
            Url = new Uri("https://github.com/seuusuario/jwinventory")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Configuração do esquema de segurança JWT no Swagger
    var jwtSecurityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Description = "Digite: Bearer {seu token}",

        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Id = "Bearer",
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme,
            Array.Empty<string>()
        }
    });

    c.EnableAnnotations();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication
var key = builder.Configuration["JwtConfig:Key"];
var issuer = builder.Configuration["JwtConfig:Issuer"];
var audience = builder.Configuration["JwtConfig:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateLifetime = true
    };
});

// Injeção de dependência
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<JwtTokenGenerator>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Redirecionar / para /swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
