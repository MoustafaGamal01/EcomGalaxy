# EcomGalaxy — E-Commerce Platform

EcomGalaxy is a full-featured e-commerce platform built with ASP.NET Core 8 MVC, designed to support multiple user roles (Admin, Seller, Customer) with a clean dark-themed UI and a well-structured backend.

🌐 **Live Demo**: [ecomgalaxy.runasp.net](https://ecomgalaxy.runasp.net)

### Demo Accounts

Use these pre-registered accounts to explore each role without signing up:

| Role     | Email                | Password     |
|----------|----------------------|--------------|
| Admin    | Admin1@gmail.com     | Aa123456@#   |
| Seller   | Seller1@gmail.com    | Aa123456@#   |
| Customer | User1@gmail.com      | Aa123456@#   |

---

## Tech Stack

**Backend**
- ASP.NET Core 8 MVC
- Entity Framework Core (Code First)
- ASP.NET Core Identity
- Repository + Service pattern (N-Tier architecture)
- Dependency Injection

**Frontend**
- Razor Views
- HTML / CSS / JavaScript
- Font Awesome icons

**Database**
- Microsoft SQL Server

---

## Architecture

EcomGalaxy follows a strict N-Tier architecture:

```
Controllers  →  Services  →  Repositories  →  DbContext (EF Core)
     ↑               ↑
  ViewModels      Domain Models
```

- **Controller Layer** — thin, handles HTTP only, no business logic
- **Service Layer** — all business rules live here
- **Repository Layer** — all DB queries live here, no logic bleeds up
- **Domain Layer** — EF Core models, DbContext
- **ViewModel Layer** — strongly typed models passed to views

---

## Features

### User Management
- Registration, login, and logout
- Role-based access control (Admin / Seller / Customer)
- Password recovery via email
- User profile management
- Session management with sliding cookie expiration

### Product Management
- Add, update, and delete products (Seller / Admin)
- Paginated product browsing (10 per page)
- Search by name, category, and description
- Filter by price range and minimum rating
- Sort by price (asc/desc) and top rated
- Filters and sort persist across pagination

### Category Management
- Add, update, and delete categories (Admin)

### Reviews & Ratings
- Customers can leave reviews and ratings on products

### Shopping Cart
- Add, update quantity, and remove items

### Order Management
- Place, cancel, and track orders
- Order statuses: Processing → Shipped → Delivered / Cancelled
- Stock quantity automatically restored on cancellation

### Payment
- Payment record created on checkout and linked to each order

---

## Running Locally

1. Clone the repository:
   ```bash
   git clone https://github.com/MoustafaGamal01/EcomGalaxy.git
   cd EcomGalaxy
   ```

2. Add `appsettings.json` to the project root (see Configuration below).

3. Apply migrations:
   ```bash
   dotnet ef database update
   ```
   Or generate a SQL script if you prefer to run it manually:
   ```bash
   dotnet ef migrations script --output migration.sql
   ```

4. Run the project:
   ```bash
   dotnet run
   ```

> Roles (Admin, Seller, Customer) are seeded automatically on first startup.

---

## Deploying to Production

1. Publish in Release mode:
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. Upload the contents of `./publish` to your hosting root.

3. Create a `logs/` folder in the site root (required by IIS for stdout logging).

4. Make sure `appsettings.json` uses your production connection string (see Configuration).

5. Set `ASPNETCORE_ENVIRONMENT` to `Production` in `web.config`:
   ```xml
   <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Production" />
   ```

---

## Configuration

Create `appsettings.json` in the project root. **Never commit this file with real credentials.**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "RemoteCS": "Server=YOUR_SERVER; Database=YOUR_DB; User Id=YOUR_USER; Password=YOUR_PASSWORD; Encrypt=True; TrustServerCertificate=True;"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": "587",
    "Username": "your-email@gmail.com",
    "Password": "your-gmail-app-password",
    "From": "your-email@gmail.com"
  }
}
```

> For Gmail, generate an **App Password** at [myaccount.google.com → Security → App passwords](https://myaccount.google.com/apppasswords). Do not use your real Gmail password.

---

## .gitignore Recommendations

Make sure these are excluded from source control:

```
appsettings.json
web.config
logs/
*.log
```

---

## Contact

For any inquiries or issues, open an issue or reach out to [@MoustafaGamal01](https://github.com/MoustafaGamal01).