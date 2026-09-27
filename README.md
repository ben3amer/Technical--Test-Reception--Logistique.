Reception Logistique - Technical Test

First commit, just the skeleton for now.

Stack
- .NET 10 (C#)
- React + TypeScript
- PostgreSQL

Architecture
- Clean Architecture (Domain / Application / Infrastructure / API)
- CQRS + MediatR (Commands = validation actions, Queries = read order/reception)
- Controllers stay thin, just call MediatR

TODO
- run instructions
- validation logic (propagation rules pallet/carton/product)
- explain gray zones choices
