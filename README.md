# WebStore - Stationary Store Management System

A modern monorepo application for managing stationary store operations, built with .NET 6 Web API backend and Next.js frontend.

## Project Structure

```
WebStore/
├── backend/                    # .NET 6 Web API Backend
│   ├── WebStore.API/          # Main API project
│   ├── WebStore.DAL/          # Data Access Layer
│   ├── WebStore.Model/        # Domain Models
│   ├── WebStore.Services/     # Business Logic Services
│   ├── WebStore.ViewModels/   # API ViewModels
│   ├── WebStore.Tests/        # Unit Tests
│   └── WebStore.Backend.sln   # Backend Solution
└── frontend/                  # Next.js Frontend
    ├── app/                   # Next.js App Router
    ├── src/                   # Source code
    │   ├── components/        # React Components
    │   ├── lib/              # Utilities and API client
    │   └── types/            # TypeScript types
    └── package.json          # Frontend dependencies
```

## Features

- **Products Management**: CRUD operations for inventory
- **Orders Management**: Track and manage customer orders
- **Store Management**: Manage multiple store locations
- **Address Management**: Handle customer and store addresses
- **Invoice Management**: Generate and manage invoices
- **Modern UI**: Built with Next.js, TypeScript, and Tailwind CSS
- **RESTful API**: Clean API design with Swagger documentation

## Getting Started

### Prerequisites

- .NET 6 SDK
- Node.js 18+ and npm
- SQLite (for development)

### Backend Setup

1. Navigate to the backend directory:

   ```bash
   cd backend
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Run the database migrations:

   ```bash
   dotnet ef database update --project WebStore.API
   ```

4. Start the API:
   ```bash
   dotnet run --project WebStore.API
   ```

The API will be available at `https://localhost:7000` with Swagger documentation at `https://localhost:7000/swagger`.

### Frontend Setup

1. Navigate to the frontend directory:

   ```bash
   cd frontend
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Create environment file:

   ```bash
   cp .env.example .env.local
   ```

4. Start the development server:
   ```bash
   npm run dev
   ```

The frontend will be available at `http://localhost:3000`.

## API Endpoints

- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

Similar endpoints are available for orders, stores, addresses, and invoices.

## Development

### Running Tests

```bash
cd backend
dotnet test
```

### Building for Production

Backend:

```bash
cd backend
dotnet publish -c Release
```

Frontend:

```bash
cd frontend
npm run build
```

## Technology Stack

### Backend

- .NET 6 Web API
- Entity Framework Core
- SQLite
- AutoMapper
- xUnit (Testing)

### Frontend

- Next.js 14
- TypeScript
- Tailwind CSS
- React 18

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License.
