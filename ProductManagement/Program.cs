using System.Reflection;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductManagement.Data;
using ProductManagement.Extensions;
using ProductManagement.Interfaces;
using ProductManagement.Interfaces.Repositories;
using ProductManagement.MediatR.Commands.Delete;
using ProductManagement.MediatR.Handlers.Commands.Create;
using ProductManagement.MediatR.Handlers.Commands.Update;
using ProductManagement.MediatR.Handlers.Queries;
using ProductManagement.MediatR.Queries;
using ProductManagement.Middlewares;
using ProductManagement.Models;
using ProductManagement.Repositories;
using ProductManagement.Services;
using ProductManagement.Validators;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(restrictedToMinimumLevel:LogEventLevel.Verbose) // Konsola yaz
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel:LogEventLevel.Warning)
    .WriteTo.PostgreSQL(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        "Logs",
        needAutoCreateTable: true,
        restrictedToMinimumLevel: LogEventLevel.Error
    )
    .CreateLogger();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));

    options.AddPolicy("SellerOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin","Seller"));
});

// Add services to the container.
builder.Services.AddDbContext<TechnobitpublicContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManagement API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient<IExcelReader, ExcelReader>();
builder.Services.AddTransient<IExcelToSellerProduct, ExcelToSellerProduct>();

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Productdetailtag>, ProductDetailTagRepository>();
builder.Services.AddScoped<IRepository<Sellerproduct>, SellerProductRepository>();
builder.Services.AddScoped<IRepository<Seller>, SellerRepository>();
builder.Services.AddScoped<IRepository<Productimage>, ProductImageRepository>();

builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductDetailTagService, ProductDetailTagService>();
builder.Services.AddScoped<ISellerProductService, SellerProductService>();
builder.Services.AddScoped<ISellerService, SellerService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionMiddleware>();
app.UseMiddleware<JwtMiddleware>(); 

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();