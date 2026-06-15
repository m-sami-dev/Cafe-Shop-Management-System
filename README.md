# ☕ Cafe Shop Management System

> **Course:** Advanced Programming (COSC-5136) | Spring 2026  
> **Domain:** Cafe Shop Management System  
> **Architecture:** App.Core + App.WindowsApp + SQL Server (ADO.NET)

---

## 👥 Group Members

| Name | GitHub Profile |
|------|---------------|
| Muhammad Sami | [@m-sami-dev](https://github.com/m-sami-dev) |
| Abdul Samad  | [@abdulsamad-as](https://github.com/abdulsamad-as) |


---

## 📁 Solution Structure

```
CafeShop.sln
├── App.Core/                           ← Class Library (Business Logic)
│   ├── Models/
│   │   ├── Product.cs
│   │   ├── Customer.cs
│   │   ├── Order.cs  (+ OrderItem)
│   │   └── FormMode.cs                ← Add / Edit / View enum
│   ├── Interfaces/
│   │   ├── IProductService.cs
│   │   ├── ICustomerService.cs
│   │   └── IOrderService.cs
│   └── Services/
│       ├── ProductService.cs           ← ADO.NET CRUD (implements IProductService)
│       ├── CustomerService.cs
│       └── OrderService.cs
│
├── App.WindowsApp/                     ← WinForms Application
│   ├── Program.cs
│   ├── App.config                      ← Connection string (not hardcoded)
│   ├── Forms/
│   │   ├── MainForm.cs + .Designer.cs  ← Shell with side-panel navigation
│   │   ├── ProductForm.cs + .Designer.cs  ← Mode-driven form (Add/Edit/View)
│   │   ├── CustomerForm.cs + .Designer.cs
│   │   └── OrderForm.cs + .Designer.cs
│   └── UserControls/
│       ├── DashboardControl.cs + .Designer.cs  ← Stats + Bar + Pie charts
│       ├── ProductsControl.cs + .Designer.cs   ← Products CRUD + search/filter
│       ├── CustomersControl.cs + .Designer.cs  ← Customers CRUD + search
│       └── OrdersControl.cs + .Designer.cs     ← Orders CRUD + status filter
│
└── Database/
    └── CafeShopDB.sql                  ← Run this first in SQL Server
```

---

## ✅ Requirements Coverage

| Requirement | Status | Detail |
|---|---|---|
| App.Core class library | ✅ | Models, Interfaces, Services |
| App.WindowsApp referencing Core | ✅ | ProjectReference in .csproj |
| 3+ entity models | ✅ | Product, Customer, Order + OrderItem |
| SQL Server database | ✅ | CafeShopDB with 4 tables |
| 3+ tables with NVARCHAR PKs | ✅ | Products, Customers, Orders, OrderItems |
| No FK constraints in DB | ✅ | Enforced at application layer only |
| Connection string in App.config | ✅ | CafeShopDB key — not hardcoded |
| DbService per entity with Interface | ✅ | ProductService, CustomerService, OrderService |
| All 5 CRUD methods | ✅ | GetAll, GetById, Add, Update, Delete |
| SqlParameter — no string concat | ✅ | Every query uses SqlParameter |
| using blocks for connections | ✅ | All connections properly wrapped |
| MainForm + side-panel navigation | ✅ | 4 nav buttons |
| One UserControl per entity | ✅ | Dashboard + 3 entity controls |
| DataGridView + BindingSource | ✅ | All 3 entity views |
| Toolbar: Add/Edit/View/Delete/Refresh | ✅ | All 5 buttons on every view |
| Mode-driven form with enum | ✅ | FormMode.Add / Edit / View |
| Validation + MessageBox feedback | ✅ | Required fields + numeric ranges |
| Confirm-before-delete | ✅ | Yes/No dialog on all deletes |
| LiveCharts2 — 2 chart types | ✅ | Bar chart + Pie chart |
| Charts update after data changes | ✅ | Refresh called after every operation |
| **RECOMMENDED:** Search + filter | ✅ | Text search + category/status dropdowns |
| **RECOMMENDED:** Dashboard | ✅ | 4 stat cards + 2 live charts |
| **RECOMMENDED:** Async operations | ✅ | GetAllAsync on all 3 entity views |
| **RECOMMENDED:** Status bar | ✅ | User, message, live clock |
| **RECOMMENDED:** Column sorting | ✅ | Click any column header to sort |

---

## 🚀 Setup Instructions

### Step 1 — Create the Database

1. Open **Visual Studio**
2. Go to **View → SQL Server Object Explorer**
3. Expand **SQL Server → (your server)**
4. Right-click **Databases → New Query**
5. Open `Database/CafeShopDB.sql`, paste contents, click **Execute**
6. You should see: *"CafeShopDB created and seeded successfully."*

### Step 2 — Fix the Connection String

Open `App.WindowsApp/App.config` and update the `Server=` value:

```xml
<!-- SQL Server Express -->
Server=.\SQLEXPRESS;Database=CafeShopDB;Integrated Security=True;

<!-- LocalDB (Visual Studio default) -->
Server=(localdb)\MSSQLLocalDB;Database=CafeShopDB;Integrated Security=True;

<!-- Full SQL Server -->
Server=localhost;Database=CafeShopDB;Integrated Security=True;
```

> **Tip:** Find your server name in SQL Server Object Explorer → right-click server → Properties → Name

### Step 3 — Install NuGet Package

Open **Tools → NuGet Package Manager → Package Manager Console** and run:

```powershell
Install-Package LiveChartsCore.SkiaSharpView.WinForms -ProjectName App.WindowsApp
```

### Step 4 — Build and Run

1. Right-click `App.WindowsApp` → **Set as Startup Project**
2. **Build → Rebuild Solution** (Ctrl+Shift+B)
3. Press **F5** to run

---

## 🖥️ Application Features

| View | Features |
|---|---|
| **Dashboard** | Total orders, revenue, pending count, customer count; Bar chart (7-day revenue); Pie chart (sales by category) |
| **Products** | Add/Edit/View/Delete; search by name or description; filter by category; click column header to sort |
| **Customers** | Add/Edit/View/Delete; search by name, phone, or email |
| **Orders** | Add/Edit/View/Delete; search by ID or customer; filter by status; rows color-coded by status |

### Adding an Order
1. Go to **Orders** → click **➕ Add**
2. Select a customer from the dropdown
3. Select a product, set quantity → click **➕ Add Item**
4. Repeat for more items
5. Set status, add optional notes → click **Save Order**

---

## 🗄️ Database Tables

```sql
Products   (ProductId NVARCHAR PK, Name, Category, Price, StockQty, Description, IsAvailable, CreatedAt)
Customers  (CustomerId NVARCHAR PK, FullName, Phone, Email, Address, LoyaltyPts, CreatedAt)
Orders     (OrderId NVARCHAR PK, CustomerId, CustomerName, TotalAmount, Status, Notes, OrderDate)
OrderItems (ItemId NVARCHAR PK, OrderId, ProductId, ProductName, Quantity, UnitPrice, SubTotal)
```

> No foreign key constraints at the database level — as per project requirements.  
> Referential integrity is enforced at the application/service layer.

---

## 🛠️ Technologies Used

| Technology | Purpose |
|---|---|
| C# .NET Framework 4.7.2 | Programming language |
| Windows Forms (WinForms) | User Interface |
| ADO.NET | Data access layer |
| SQL Server LocalDB `(localdb)\MSSQLLocalDB` | Database |
| LiveChartsCore.SkiaSharpView.WinForms | Charts (Bar + Pie) |
| Visual Studio 2022 | IDE |
