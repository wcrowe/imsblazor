# imsblazor

A production-ready .NET 8 Blazor Server application with:

- ✅ **Windows Authentication**
- ✅ **Serilog** with `UserId` column logged to SQL
- ✅ **Dapper** using **only stored procedures**
- ✅ **Fluxor** for state management
- ✅ **MediatR** for CQRS-style handlers
- ✅ **Razor PDF** and CSV export
- ✅ **Audit logging**, alerting, archiving, and retention
- ✅ **Markdown + PDF reports**, GitHub Wiki integration
- ✅ Modular structure, `src/` layout, `global.json` for LTS

---

## 📦 Features
- **/reports** page with weekly audit summary PDF and CSV
- **/permissions** page with `[Authorize(Roles = "Admin")]`
- **/rendermodes-demo** page showcasing Static + Interactive render modes
- **Background services** for:
  - Log retention
  - Log archiving
  - Email/SMS alerts
- **Markdown export** auto-syncs to GitHub Wiki

---

## 🚀 Getting Started

```bash
git clone https://github.com/wcrowe/imsblazor.git
cd imsblazor
```

### Prerequisites
- ✅ .NET 8 SDK (pinned via `global.json`)
- ✅ SQL Server with `LoggingDB` and `imsblazor` databases
- ✅ Visual Studio 2022+ or VS Code

### Setup
```bash
dotnet restore
cd src
dotnet build
```

To run:
```bash
dotnet run --project src/imsblazor
```

---

## 🛠 Configuration

Edit `appsettings.json`:
```json
"ConnectionStrings": {
  "ImsDatabase": "Server=.;Database=imsblazor;Integrated Security=True;",
  "LoggingDatabase": "Server=.;Database=LoggingDB;Integrated Security=True;"
}
```

## 📄 SQL Setup
Run [`Setup_All_SQL.sql`](./src/sql/Setup_All_SQL.sql) to:
- Create `Logs` + `LogArchive`
- Add stored procedures:
  - `sp_GetWeeklyLogSummary`
  - `sp_GetAuditLogsForExport`
  - `sp_LogAudit`
  - etc.

---

## 🧪 Tests
> Coming soon: Unit test project

---

## 📦 Build & Deploy (Local)
```bash
dotnet publish -c Release -o ./publish
```

Deploy to `C:\inetpub\wwwroot\imsblazor` on IIS with Hosting Bundle.
See [Deployment Guide](https://github.com/wcrowe/imsblazor/wiki) for full instructions.

---

## 📜 License
[MIT](LICENSE)

---

## 👨‍💻 Maintainer
Created by [@wcrowe](https://github.com/wcrowe)
