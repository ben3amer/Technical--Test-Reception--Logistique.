# Réception Logistique — Backend

.NET 10 REST API for logistics reception management.

## stack

- .NET 10 / ASP.NET Core
- PostgreSQL + Entity Framework Core
- Clean Architecture + CQRS + MediatR
- xUnit + NSubstitute (unit tests)

## how to run

**Prerequisites:** Docker, .NET 10 SDK

**1. Start the database**
```bash
docker run --name reception-db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=yourpassword -e POSTGRES_DB=ReceptionLogistique -p 5432:5432 -d postgres:16
```

**2. Update the connection string** in `ReceptionLogistique.WebApi/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=ReceptionLogistique;Username=postgres;Password=yourpassword"
}
```

**3. Run migrations + start**
```bash
dotnet ef migrations add InitialCreate --project ReceptionLogistique.Infrastructure --startup-project ReceptionLogistique.WebApi
dotnet run --project ReceptionLogistique.WebApi
```

App starts at `http://localhost:5164` — Swagger at `http://localhost:5164/swagger`.

Seeded order ID: **CMD-2026**

**4. Run tests**
```bash
dotnet test
```

## architecture decisions

**Clean Architecture** — Domain → Application → Infrastructure → WebApi. Business rules live in the domain (entities validate themselves), handlers in Application, DB in Infrastructure. Nothing leaks upward.

**CQRS with MediatR** — reads and writes are separate. `GetDeliveryQuery` for reads, one command per mutation. Handlers are thin: fetch aggregate, call domain method, save.

**Single aggregate repository** — `IDeliveryRepository` only. `Delivery` is the aggregate root; `Pallet`, `Carton`, `Product` are always accessed through it. No per-entity repos — that would break the aggregate boundary.

**Validation at domain level** — `Delivery`, `Pallet`, `Carton` constructors throw `ArgumentException` on invalid input. The middleware maps those to 400, `KeyNotFoundException` to 404.

## grey areas & decisions

**Saving on each click vs at the end** — chose save-on-each-click. A logistics reception is an operation where losing progress (network drop, crash) would mean re-scanning everything. Immediate persistence is safer and simpler than a "commit" step.

**Quantity handling** — spec says `expectedQuantity`. Chose binary receive/unreceive (received = full expected quantity, unreceived = 0) rather than partial quantity input. Simpler UX for a warehouse worker scanning items, and matches the user story wording ("validate a product").
