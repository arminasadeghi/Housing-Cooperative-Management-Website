# Housing Cooperative website-backend

## overview

Backend service for managing a **housing cooperative**: customers, land projects, plots, contracts, and payments.  
Built with **ASP.NET Core 6**, **EF Core (SQL Server)**, **CQRS (Commands/Queries)**, and **DDD-inspired** patterns.

## Features

- **Customer management** – register users, update profile, fetch customer info.
- **Land projects & plots** – list projects, phases, and plots with paging support.
- **Contracts** – create and manage housing contracts for customers.
- **Payments** – record and query payments, including basic admin views.
- **API versioning** – versioned endpoints under `api/v{version}/...`.
- **Observability** – Serilog logging; `/ErrorsList` endpoint for error codes.
- **Documentation** – Swagger/OpenAPI enabled in non‑production environments.

## Tech Stack

- **Language**: C#, .NET 6
- **Framework**: ASP.NET Core Web API
- **Data**: Entity Framework Core + SQL Server (migrations included)
- **Architecture**: CQRS (Commands / Queries), MediatR, FluentValidation
- **Mapping**: Mapster (DTO & mapper generation)
- **Messaging (optional)**: RabbitMQ event bus
- **Service discovery (optional)**: Consul
- **Auth**: JWT Bearer with external IdentityServer