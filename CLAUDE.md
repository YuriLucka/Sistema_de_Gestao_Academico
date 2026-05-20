# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```powershell
# Build
dotnet build CAA/CAA.csproj

# Run (dev)
dotnet run --project CAA/CAA.csproj

# EF Core migrations
dotnet ef migrations add <NomeMigration> --project CAA/CAA.csproj
dotnet ef database update --project CAA/CAA.csproj

# Publish
dotnet publish CAA/CAA.csproj -c Release
```

No test project exists in this repository.

## Architecture

**Stack:** ASP.NET Core 9 MVC + Razor Pages, Entity Framework Core (SQL Server), ASP.NET Core Identity, SignalR.

**Dual layout system — critical to understand:**

- **Identity/Login pages** (`Areas/Identity/`) use `Views/Shared/_Layout.cshtml` — a custom two-panel layout with Tailwind CSS (CDN), Lucide Icons, Inter font, and a green WhatsApp-inspired palette (`#25D366`, `#128C7E`). No Bootstrap here.
- **Authenticated app pages** (`Views/`) use `Views/Shared/AdminLTE/_Layout.cshtml` — AdminLTE 4 with Bootstrap, served from `wwwroot/lib/admin-lte/`.

When editing UI, confirm which layout applies before touching CSS.

**Authentication & Authorization:**
- `Usuario` extends `IdentityUser` with extra fields: `Nome`, `Sobrenome`, `Cargo`, `Departamento`, `FotoPerfil`, `Ativo`.
- Login requires `EmailConfirmed = true` (enforced in `Program.cs`).
- Role-based access: roles are named strings (e.g. `"Admin"`, `"Matrículas"`, `"Estágios"`). Controllers use `[Authorize(Roles = "...")]`. The sidebar in `_MainNavigation.cshtml` conditionally renders items per role.
- Seeding runs on every startup via `SeedDataBase.SeedAll()` — idempotent. Admin credentials: `admin@anhembisorocaba.com.br` / `Admin@123`.

**Controllers:**
- Most extend `BaseController` which injects `ApplicationDbContext` + `UserManager<Usuario>`.
- `MatriculaController` and others that don't need `UserManager` inject `ApplicationDbContext` directly.
- `HomeController` is the post-login landing page (`/`), requires `[Authorize]`.

**Data layer:**
- `ApplicationDbContext` extends `IdentityDbContext<Usuario>`.
- Global cascade delete is disabled — all FK relations use `DeleteBehavior.Restrict`.
- `decimal(18,2)` is configured explicitly for `Desconto.Valor`, `PlanoFinanceiro.Valor`, `TipoDesconto.ValorPadrao`.

**Real-time chat:** `ChatHub` (SignalR) at `/chathub`. The main layout injects a global SignalR connection that shows SweetAlert2 toast notifications for incoming messages when not on the `/chat` route.

**Email:** `EmailSender` (SMTP via Gmail) is registered as `IEmailSender`. SMTP config lives in `appsettings.json` under `"Smtp"`.

**Localization:** Fixed to `pt-BR` globally (dates, numbers, etc).

**Static assets:** `wwwroot/` — AdminLTE, Bootstrap, Bootstrap Icons, DataTables, ApexCharts, OverlayScrollbars, jsvectormap all vendored locally. Tailwind and Lucide are loaded from CDN only on Identity pages.
