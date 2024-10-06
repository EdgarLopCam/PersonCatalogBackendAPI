# PersonCatalogBackendAPI

This API allows you to manage a catalog of persons with operations such as Create, Read, Update, and Delete (CRUD). It is built using .NET 6 and C#.

## Prerequisites

Before running the project, ensure you have the following installed on your machine:

- [.NET SDK 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MySQL](https://www.mysql.com/downloads/) (if using MySQL as a database)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (if using SQL Server as a database)

note: For this example we use SQL Server with Entity Framework Code First, The code is the same for both database managers, 
the only thing I would change is the plugin, each one has its own library

## Setup Instructions

### Step 1: Clone the Repository

Clone the repository to your local machine using the following command:

```bash
git clone https://your-repository-url.git
```

### Step 2: Navigate to the Project Directory

Move to the project directory where the `PersonCatalog` API is located:

```bash
cd PersonCatalog/PersonCatalogAPI
```

### Step 3: Install Dependencies

Restore the necessary dependencies for the API by running:

```bash
dotnet restore
```

### Step 4: Apply Migrations (if using Entity Framework)

If you are using Entity Framework with Code First, apply the migrations to create the database schema:

```bash
dotnet ef migrations add InitialCreate -s PersonCatalog -p Infrastructure
dotnet ef database update -s PersonCatalog -p Infrastructure
```

### Step 5: Configure CORS

CORS (Cross-Origin Resource Sharing) is enabled for specific URLs in the `Program.cs` file to allow the frontend (or other clients) to communicate with the API. The following URLs are configured for CORS:

- `http://localhost:4200` (For local Angular frontend)
- Any other specific URL that you want to allow

Here’s the CORS configuration snippet from `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
```

Make sure the URLs in the `WithOrigins` method match the URLs where your frontend or clients are running.

### Step 6: Run the API

To run the API locally, use the following command:

```bash
dotnet run
```

This will start the API, and you can access it at the default URL:

```
http://localhost:5000
```

If you’re running HTTPS, it will be:

```
https://localhost:5001
```

You can also run the project directly from your IDE.

### Step 7: Access the API Documentation (Swagger)

Swagger is configured to document and test the API endpoints. Once the API is running, open your browser and navigate to:

```
http://localhost:5000/swagger
```

### Step 8: Build and Test

To build the project:

```bash
dotnet build
```

To run the tests (if available):

```bash
dotnet test
```

## Common Issues

1. **CORS Policy Issues:**
   - If you encounter CORS-related issues, ensure that the frontend URL is correctly added in the `WithOrigins` method in the CORS configuration.

2. **Database Connection:**
   - Ensure that the connection string in the `appsettings.json` is correctly set for your MySQL or other database provider.

## Additional Configuration(ConectionString)

```csharp
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=XXXX;Persist Security Info=False;Initial Catalog=PersonCatalogDb;User ID=sa;Password=XXXX;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
