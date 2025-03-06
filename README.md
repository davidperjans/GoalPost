# GoalPost

Ett webbapplikationsprojekt byggt med ASP.NET Core Web API och React.

## Teknisk Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- Identity Framework
- MediatR (CQRS)
- FluentValidation
- JWT Authentication
- SQL Server

### Frontend (kommande)
- React
- TypeScript
- Zustand
- TailwindCSS
- ShadcnUI

## Installation

1. Klona repot
2. Öppna solution i Visual Studio
3. Uppdatera connection string i `appsettings.json`
4. Kör följande kommandon i Package Manager Console:
   ```
   Update-Database
   ```
5. Starta projektet

## Utveckling

Projektet följer Clean Architecture-principer med följande lager:
- Core (Domain entities, interfaces)
- Infrastructure (Data access, external services)
- Application (Business logic, CQRS handlers)
- API (Controllers, middleware) 