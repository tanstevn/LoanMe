## Requirements

- .NET 10 SDK
- Microsoft SQL Server

## How to run

Make sure your terminal is pointing to:

```bash
../LoanMe/Backend
```

Ensure dependencies are installed by running:

```bash
dotnet clean; dotnet restore; dotnet build;
```

Run the local server with:

```bash
dotnet run --project ./LoanMe.Api
```

## API endpoints

You can check the endpoints and their respective request and response models when you go to:

```bash
http://localhost:5145/scalar/v1
```