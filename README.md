# CRM SYSTEM

A simple **CRM (Customer Relationship Management) console application** built with **C# and .NET 8**. The project provides basic functionality for managing leads, customers, opportunities, activities, notes, and reports within a text-based menu system.

---

## 📌 Features

* **Customer Management**

  * Add, update, list, delete customers
  * Convert leads into customers
* **Lead Management**

  * Create, update, list, delete leads
  * Track lead status
* **Opportunity Management**

  * Manage sales opportunities
  * Mark opportunities as won/lost
* **Activity & Notes**

  * Record notes and activities related to leads/customers
* **Reports**

  * View summaries (e.g., revenue from won opportunities)

---

## 🛠️ Project Structure

```
CRM-SYSTEM/
 └── CRM Project/
      ├── CRM Project.sln                # Solution file
      ├── CRM Project.csproj             # Project file
      ├── Program.cs                     # Entry point (menu loop)
      ├── Clasess/                       # Core classes & helpers
      ├── Customer/                      # Customer domain logic
      ├── Lead/                          # Lead domain logic
      ├── Opportunity/                   # Opportunity domain logic
      ├── Interface/                     # Interfaces (IMenu, INotifier, IClock)
```

---

## 🚀 Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Run the Application

```bash
# Navigate into the project folder
cd "CRM-SYSTEM/CRM  Project/CRM  Project"

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

---

## 📂 Main Namespaces

* `CRM__Project.Clasess` → Core classes (helpers, paging, reports, etc.)
* `CRM__Project.Customer` → Customer logic & services
* `CRM__Project.Lead` → Lead management
* `CRM__Project.Opportunity` → Opportunity handling
* `CRM__Project.Interface` → Interfaces for menus, notifications, and clock services

---

## ✨ Future Improvements

* Add persistent storage (e.g., database or file saving)
* Implement authentication & roles
* Provide a GUI or web interface (ASP.NET MVC/Blazor)

---

## 📄 License

This project is for **learning purposes** and not intended for production use.
