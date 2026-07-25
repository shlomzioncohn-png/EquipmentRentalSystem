# Rental Business Management Platform

A full-stack web application for managing local rental businesses and their inventory — business owners can register their business, list rentable items, and manage them from a dashboard, while customers can search for businesses on an interactive map and browse available items.

## Overview

The system is built as a **layered .NET backend** (Core / Data / Service / WebApi) exposing a **RESTful API**, paired with an **Angular single-page application** as the client. It supports full CRUD management of businesses, users, and items, user authentication, and geographic business discovery via Google Maps.

## Tech Stack

**Backend**
- C# / ASP.NET Core 9 Web API
- Entity Framework Core 9 (Code-First, SQL Server)
- AutoMapper for DTO ↔ entity mapping
- Layered architecture: `Core` (models, interfaces, DTOs) · `Data` (EF DbContext & repositories) · `Service` (business logic) · `WebApi` (REST controllers)
- Repository pattern with dependency injection
- Swagger / OpenAPI for API documentation
- CORS configured for the Angular client

**Frontend**
- Angular 21 (standalone components)
- TypeScript, RxJS
- Google Maps JavaScript API integration for business location display
- Component-based architecture with a dedicated services layer for HTTP communication

## Key Features

- **User authentication** — registration and login with email/password validation
- **Business management** — create, update, delete, and list businesses, including address, geolocation (latitude/longitude), operating hours, and contact details
- **Item/inventory management** — add, edit, and remove items for rent per business, including price, availability, and return terms
- **Map-based discovery** — businesses are plotted on an interactive Google Map for easy location and navigation
- **Ownership rules** — a business can only be deleted once all its associated items have been removed, and a user's businesses are grouped and manageable from a personal dashboard ("My Businesses")
- **Data validation** — server-side model validation (required fields, email format, etc.) with clear API error responses

## Architecture

```
Client (Angular)  ──HTTP/REST──▶  WebApi (Controllers)
                                        │
                                     Service (business logic)
                                        │
                                     Core (interfaces, models, DTOs, AutoMapper profiles)
                                        │
                                     Data (EF Core DbContext, repositories, migrations)
                                        │
                                   SQL Server Database
```

### Backend Modules
| Project | Responsibility |
|---|---|
| `Core` | Domain models (`User`, `Business`, `Item`), DTOs/Resources, repository & service interfaces, AutoMapper profiles |
| `Data` | EF Core `DbContext`, concrete repositories, database migrations |
| `Service` | Business logic implementation (`UserService`, `BusinessService`, `ItemService`, `EmailService`) |
| `WebApi` | REST API controllers, DI configuration, Swagger, CORS |

### Frontend Modules
Key Angular components: `home`, `nav`, `login`, `register`, `business-list`, `business-detail`, `add-business`, `my-businesses`, `items-list`, `add-item`, `map` (Google Maps integration).

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server (LocalDB or full instance)
- Node.js + npm
- Angular CLI 21

### Backend
```bash
cd WebApi
dotnet restore
dotnet ef database update
dotnet run
```
The API will be available at `https://localhost:xxxx` with Swagger UI at `/swagger`.

### Frontend
```bash
cd rentalBusinessProjectClient
npm install
ng serve --open
```
The client runs at `http://localhost:4200` and communicates with the API (CORS-enabled).

## API Endpoints (examples)

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/business` | Get all businesses |
| GET | `/api/business/{id}` | Get business by ID |
| GET | `/api/business/user/{userId}` | Get businesses owned by a user |
| POST | `/api/business` | Create a new business |
| PUT | `/api/business/{id}` | Update a business |
| DELETE | `/api/business/{id}` | Delete a business (only if it has no items) |
| GET | `/api/item` | Get all items |
| GET | `/api/item/business/{businessId}` | Get items by business |
| POST | `/api/item` | Create a new item |
| POST | `/api/user/login` | User login |

