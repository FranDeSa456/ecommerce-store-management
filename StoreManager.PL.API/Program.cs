using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StoreManager.BLL.Models;
using StoreManager.BLL.Services;
using StoreManager.BLL.Services.Interfaces;
using StoreManager.DAL.Data;
using StoreManager.DAL.Entities;
using StoreManager.DAL.Repositories;
using StoreManager.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

builder.Services.AddScoped<IGenericService<CustomerModel>, GenericService<Customer, CustomerModel>>();
builder.Services.AddScoped<IGenericService<AddressModel>, GenericService<Address, AddressModel>>();
builder.Services.AddScoped<IGenericService<CategoryModel>, GenericService<Category, CategoryModel>>();
builder.Services.AddScoped<IGenericService<ProductModel>, GenericService<Product, ProductModel>>();
builder.Services.AddScoped<IGenericService<OrderModel>, GenericService<Order, OrderModel>>();
builder.Services.AddScoped<IGenericService<PaymentModel>, GenericService<Payment, PaymentModel>>();
builder.Services.AddScoped<IGenericService<ReviewModel>, GenericService<Review, ReviewModel>>();

builder.Services.AddScoped<IOrderItemService, OrderItemService>();

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
