# Consistent System — Phase 0 (Project Setup)

This skeleton targets **.NET 8** with **CoreWCF** for server-side WCF compatibility.

## Projects
- `Shared.Contracts` — Service contracts & DTOs (CoreWCF attributes).
- `Sensor.Service` — Sensor implementation (placeholder logic in Phase 0).
- `Sensor.S1.Host` / `Sensor.S2.Host` / `Sensor.S3.Host` — CoreWCF HTTP hosts for each sensor.
- `Coordinator.Host` — CoreWCF host for reconciliation service (stub for now).
- `Client.CLI` — Console client (uses WCF client packages).

## Ports
- S1: `http://localhost:5011/sensor`
- S2: `http://localhost:5012/sensor`
- S3: `http://localhost:5013/sensor`
- Coordinator: `http://localhost:5020/coord`

## Build & Run (CLI)
```bash
# 1) Create a solution and add projects
dotnet new sln -n ConsistentSystem
dotnet sln add src/Shared.Contracts/Shared.Contracts.csproj
dotnet sln add src/Sensor.Service/Sensor.Service.csproj
dotnet sln add src/Sensor.S1.Host/Sensor.S1.Host.csproj
dotnet sln add src/Sensor.S2.Host/Sensor.S2.Host.csproj
dotnet sln add src/Sensor.S3.Host/Sensor.S3.Host.csproj
dotnet sln add src/Coordinator.Host/Coordinator.Host.csproj
dotnet sln add src/Client.CLI/Client.CLI.csproj

# 2) Restore + build
dotnet restore
dotnet build -c Debug

# 3) Run hosts (in separate terminals)
dotnet run --project src/Sensor.S1.Host
dotnet run --project src/Sensor.S2.Host
dotnet run --project src/Sensor.S3.Host
dotnet run --project src/Coordinator.Host

# 4) Test the client
dotnet run --project src/Client.CLI
```

> Note: NuGet package versions may evolve; if restore fails, update to the latest **CoreWCF** and **System.ServiceModel** package versions.

## Next (Phase 1 preview)
- Add EF Core (per-sensor DB contexts), entities, and migrations.
- Replace stub logic in `SensorService` with background sampling loop and DB writes.
- Keep contracts stable; implementations evolve behind them.
