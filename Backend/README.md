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