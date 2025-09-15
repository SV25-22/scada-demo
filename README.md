# Consistent System — Phase 1 (EF Core + SQLite + background sampler)

This is a ready-to-run upgrade of Phase 0:
- Each sensor writes to its own SQLite DB (`s1.db`, `s2.db`, `s3.db`) created under a local `data/` folder.
- A background worker writes a new reading every 1–10 seconds.
- `GetLatest` and `GetSnapshot` query the database.
- `Start()` / `Stop()` flip a shared sampling switch.

## Build
```bash
dotnet new sln -n ConsistentSystem
dotnet sln add src/Shared.Contracts/Shared.Contracts.csproj
dotnet sln add src/Sensor.Service/Sensor.Service.csproj
dotnet sln add src/Sensor.S1.Host/Sensor.S1.Host.csproj
dotnet sln add src/Sensor.S2.Host/Sensor.S2.Host.csproj
dotnet sln add src/Sensor.S3.Host/Sensor.S3.Host.csproj
dotnet sln add src/Coordinator.Host/Coordinator.Host.csproj
dotnet sln add src/Client.CLI/Client.CLI.csproj

dotnet restore
dotnet build -c Debug
```

## Run (four terminals)
```bash
dotnet run --project src/Sensor.S1.Host
dotnet run --project src/Sensor.S2.Host
dotnet run --project src/Sensor.S3.Host
dotnet run --project src/Coordinator.Host
```

## Client probe
```bash
dotnet run --project src/Client.CLI
```

## Where are the DBs?
Each host writes under its own `./data/` folder (beside the executable). The schema is created on first run via `EnsureCreated()`.
