# Ecommerce Store Management

A small ASP.NET Core Web API that models a simplified e-commerce domain (customers, addresses, products, categories, orders, order items, payments and reviews). It was built as a hands-on exercise to practice modeling relational data with Entity Framework Core and to structure a .NET solution using a layered (Clean Architecture) approach.

## Technologies

- **.NET 10** / ASP.NET Core
- **Entity Framework Core 10** (SQL Server provider, LocalDB)
- **AutoMapper 16** for entity ↔ DTO mapping
- **Microsoft.AspNetCore.OpenAPI** + **Scalar** as the interactive API docs UI
- **MSSQLLocalDB** as the local database engine

## Architecture

The solution is split into four projects to separate concerns:

| Project | Layer | Responsibility |
| --- | --- | --- |
| `StoreManager.DAL` | Data Access | EF Core entities, `StoreDbContext`, migrations, generic repository + unit of work |
| `StoreManager.BLL` | Business Logic | DTO/model classes, generic service layer, AutoMapper-driven mapping |
| `StoreManager.PL.API` | Presentation (REST) | REST controllers, dependency injection, OpenAPI/Scalar wiring |
| `StoreManager.PL.MVC` | Presentation (MVC) | MVC scaffolding placeholder for a future web UI |

The API follows a **generic repository + generic service** pattern: one
`GenericRepository<TEntity>` / `GenericService<TEntity, TModel>` pair serves
every entity, with a dedicated `OrderItemRepository` / `OrderItemService` for
the composite-key entity.

## Features

- **Fluent API configuration** in `OnModelCreating` for keys, relationships and constraints (data annotations are also used on entities for `Required`, `MaxLength`, `Precision`).
- **One-to-one** — `Order ↔ Payment`, with `Payment` as the dependent holding the FK (`OrderId`) and a unique index enforcing the 1:1.
- **One-to-many** — `Customer → Address`, `Customer → Order`, `Category → Product`, `Product → Review`, `Order → OrderItem`.
- **Many-to-many with payload** — `Product ↔ Order` is modeled through the explicit join entity `OrderItem`, which carries a composite primary key `(ProductId, OrderId)` plus payload columns `Quantity` and `UnitPrice`.
- **Composite primary key** on `OrderItem` configured via `HasKey`.
- **Table-level CHECK constraints** added through `ToTable(...)`:
  - `Payment.Amount > 0`
  - `OrderItem.Quantity > 0` and `OrderItem.UnitPrice > 0`
  - `Product.Price > 0` and `Product.Stock >= 0`
  - `Review.Rating BETWEEN 1 AND 5`
- RESTful controllers per entity (`/api/customer`, `/api/product`, `/api/order`, `/api/payment`, …) with `GET`, `GET/{id}`, `POST`, `PUT/{id}`, `DELETE/{id}`.
- `CreatedAtAction` for proper `201 Created` responses with a `Location`
  header.
- `[ApiController]` automatic model validation (`400 ProblemDetails`).
- Scalar API reference UI served at `/scalar` in Development.

## What I learned

This project was built as a practical exercise to consolidate what I learned about Entity Framework Core and ASP.NET Core. Through it I practiced, among other things:

- Modeling the main relationship cardinalities (1:1, 1:N, M:N with payload) and seeing how each is expressed in the Fluent API versus by convention.
- Choosing the dependent side of a 1:1 relationship and understanding how the FK + unique index enforce it at the database level.
- Using an explicit join entity with a composite key and payload columns instead of a bare skip-navigation M:N, to carry extra data per link.
- Adding CHECK constraints via `ToTable(...)` and reflecting on where validation should live (DB vs. model vs. service).
- Structuring a .NET solution into layers and wiring up a generic repository/service pipeline with AutoMapper.
- Documenting and testing HTTP endpoints interactively with Scalar.

Future improvements include configuring the repositories for lazy loading so related navigation properties load on demand, a few more CHECK constraints I left out for now, and more granular controller attributes like (ProducesResponseType per status code) so API consumers get clearer, more specific error information instead of a generic 400 or 500.

## Running the Project

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- **SQL Server Express LocalDB** (installed with Visual Studio or the .NET workload; the connection string uses `(localdb)\MSSQLLocalDB`).

### Steps

1. **Clone the repository**
  ```bash
  git clone https://github.com/FranDeSa456/ecommerce-store-management.git
  cd ecommerce-store-management
  ```
2. Restore dependencies
  ```bash
  dotnet restore
  ```
3. Apply the database migration
  ```bash
  dotnet ef database update --project StoreManager.DAL --startup-project StoreManager.PL.API
  ```
  This creates the StoreManager database on LocalDB and applies `InitialCreate`

4. Run the API
  ```bash
  dotnet run --project StoreManager.PL.API
  ```
5. Open the API docs
Navigate to https://localhost:7094/scalar (the https profile auto-opens Scalar in the browser on launch) for a quick Endpoint Method reference
