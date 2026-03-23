# WebApplicationASP1 — Product Catalog API

ASP.NET Core Web API (.NET 10) with MongoDB for managing an e-commerce product catalog.

## Project structure

```
WebApplicationASP1/
├── Controllers/
│   └── ProductsController.cs
├── Models/
│   └── Product.cs
├── Services/
│   └── ProductService.cs
├── Settings/
│   └── MongoDbSettings.cs
├── appsettings.json
├── Program.cs
└── README.md
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/try/download/community) running on `localhost:27017`

## Getting started

```bash
# Restore packages
dotnet restore

# Run the application
dotnet run
```

The API will start at `https://localhost:{port}`.
OpenAPI schema is available at `/openapi/v1.json` (Development only).

## Configuration

MongoDB connection is configured in `appsettings.json`:

```json
"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "ProductCatalog",
  "ProductsCollectionName": "Products"
}
```

For production, override `ConnectionString` via an environment variable or user secrets:

```bash
dotnet user-secrets set "MongoDbSettings:ConnectionString" "mongodb+srv://user:pass@cluster.mongodb.net"
```

## API endpoints

| Method   | Route                  | Description          |
|----------|------------------------|----------------------|
| `GET`    | `/api/products`        | List all products    |
| `GET`    | `/api/products/{id}`   | Get product by ID    |
| `POST`   | `/api/products`        | Create a product     |
| `PUT`    | `/api/products/{id}`   | Replace a product    |
| `DELETE` | `/api/products/{id}`   | Delete a product     |

## Example request

```http
POST /api/products
Content-Type: application/json

{
  "name": "Gaming Mouse",
  "description": "RGB gaming mouse",
  "price": 49.99,
  "category": "Electronics",
  "stock": 120
}
```

## Product document

```json
{
  "_id": "ObjectId",
  "name": "Gaming Mouse",
  "description": "RGB gaming mouse",
  "price": 49.99,
  "category": "Electronics",
  "stock": 120,
  "createdAt": "2026-03-10T00:00:00Z"
}
```
