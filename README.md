# Ecommerce MVC
EcomGalaxy, a robust e-commerce platform designed to deliver a seamless shopping experience. This project showcases a modern e-commerce website built using the latest web technologies, featuring a user-friendly interface and efficient back-end functionalities.

## Technologies Used
Backend
* ASP.NET Core 8
* Entity Framework Core
* ASP.NET MVC
* Identity for User Management
* AutoMapper

Frontend
* HTML
* CSS
* Java Script

Databases
* MS SQL Server

## Architecture
EComGalaxy follows N-tier architecture, which includes:

* **Business Layer**: Implements core business logic.
* **Data Access Layer**: Utilizes the Repository and Unit of Work patterns for efficient data retrieval.
* **Repository Pattern**: Organizes data access logic.
* **Dependency Injection**: Enhances code modularity and testability.

### User Management and Authentication
* User registration and login.
* User Roles (Admin, Seller, Cutsomer).
* Password Recovery.
* Manage User Profiles Info.
* Session Management.
* Role-Based Access Control.

### Products Management
* Add, update, and delete products.
* Search Products based on their (Name, Category, Description).
* Filter Products based on their (Price, Rate) .

### Categories Management
* Add, update, and delete Categories.

### Reviews and Rating Management
* Add, update rates.

### Shopping Cart Management
* Create, update, and delete shopping cart.

### Order Management
* Create, update, and cancel orders.
* Track and Manage order status (Proccessing, Shipped, Delivered..etc)

## Running the Project
To run EcomGalaxy MVC Project:

1. Clone the repository.
2. Add `appsettings.json` File.
3. Configure connection strings in `appsettings.json` for database interaction.
4. Configure Email Settings in `appsettings.json` for sending emails (email confirmation).
5. Run database migrations to initialize the data structure.
6. Build and run the application.

## Configuration
Ensure that your `appsettings.json` is correctly set up for your environment. Here is an example:

```json
{
    "ConnectionStrings":{
    "MyCS": "data source=; Initial Catalog=; integrated Security=; Encrypt=; TrustServerCertificate=;"
    },
    "EmailSettings": {
    "SmtpServer": "smtp.gmail.com", // for Gmail
    "Port": "587",
    "Username": "Your Name/Email Here@gmail.com",
    "Password": "ThePasswordHere",
    "From": "YourEmailHere@gmail.com"
    }
}
```

## Contact
For any inquiries or issues, please contact the repository owner @MoustafaGamal01.
