using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;

using ProductManagement.Data;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Delete;
using ProductManagement.MediatR.Handlers.Commands.Create;
using ProductManagement.MediatR.Handlers.Commands.Update;
using ProductManagement.MediatR.Handlers.Queries;
using ProductManagement.MediatR.Queries;
using ProductManagement.Models;
using ProductManagement.Repositories;
using ProductManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TechnobitpublicContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Productdetailtag>, ProductDetailTagRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductDetailTagService, ProductDetailTagService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();