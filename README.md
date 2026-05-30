# ☕ Cafe Shop Management System
### COSC-5136 Advanced Programming | Spring 2026

---

## 📁 Solution Structure

```
CafeShop.sln
├── App.Core/                          ← Class Library (Business Logic)
│   ├── Models/
│   │   ├── Product.cs
│   │   ├── Customer.cs
│   │   ├── Order.cs  (+ OrderItem)
│   │   └── FormMode.cs               ← Add / Edit / View enum
│   ├── Interfaces/
│   │   ├── IProductService.cs
│   │   ├── ICustomerService.cs
│   │   └── IOrderService.cs
│   └── Services/
│       ├── ProductService.cs          ← ADO.NET CRUD (implements IProductService)
│       ├── CustomerService.cs
│       └── OrderService.cs
│
├── App.WindowsApp/                    ← WinForms Application
│   ├── Program.cs
│   ├── App.config                     ← Connection string lives here
│   ├── Forms/
│   │   ├── MainForm.cs               ← Shell with side-panel navigation
│   │   ├── ProductForm.cs            ← Mode-driven form (Add/Edit/View)
│   │   ├── CustomerForm.cs
│   │   └── OrderForm.cs
│   └── UserControls/
│       ├── DashboardControl.cs       ← Stats + Bar + Pie charts
│       ├── ProductsControl.cs        ← Products CRUD + search/filter
│       ├── CustomersControl.cs       ← Customers CRUD + search
│       └── OrdersControl.cs         ← Orders CRUD + status filter
│
└── Database/
    └── CafeShopDB.sql                ← Run this first in SQL Server
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
| No FK constraints in DB | ✅ | Enforced at app layer only |
| Connection string in App.config | ✅ | CafeShopDB key in config |
| DbService per entity (IInterface) | ✅ | ProductService, CustomerService, OrderService |
| All 5 CRUD methods | ✅ | GetAll, GetById, Add, Update, Delete |
| SqlParameter (no string concat) | ✅ | Every query uses SqlParameter |
| using blocks for connections | ✅ | All connections wrapped |
| MainForm + side-panel navigation | ✅ | 4 nav buttons (Dashboard, Products, Customers, Orders) |
| One UserControl per entity | ✅ | DashboardControl + 3 entity controls |
| DataGridView + BindingSource | ✅ | All 3 entity views |
| Toolbar: Add/Edit/View/Delete/Refresh | ✅ | All 5 buttons |
| Mode-driven form with enum | ✅ | FormMode.Add / Edit / View |
| Validation + MessageBox feedback | ✅ | Required fields + numeric ranges |
| Confirm-before-delete | ✅ | Yes/No dialog on all deletes |
| LiveCharts2 (2 chart types) | ✅ | Bar (daily revenue) + Pie (sales by category) |
| Charts update after data changes | ✅ | LoadData() called after every CUD operation |
| **RECOMMENDED:** Search + filter | ✅ | Text search + category/status dropdowns |
| **RECOMMENDED:** Dashboard | ✅ | 4 stat cards + 2 charts |
| **RECOMMENDED:** Async operations | ✅ | GetAllAsync on all 3 entity views |
| **RECOMMENDED:** Status bar | ✅ | User, message, live clock |
| **RECOMMENDED:** Column sorting | ✅ | Click header to sort asc/desc |

---

## 🚀 Setup Instructions (Step by Step)

### Step 1 — Create the Database

1. Open **Visual Studio**
2. Go to **View → SQL Server Object Explorer**
3. Expand **SQL Server → (your server)**
4. Right-click **Databases → New Query**
5. Open the file `Database/CafeShopDB.sql` and paste its contents
6. Click **Execute** (or press F5)
7. You should see: *"CafeShopDB created and seeded successfully."*

### Step 2 — Fix the Connection String

Open `App.WindowsApp/App.config` and edit the `Server=` part:

```xml
<!-- If using SQL Server Express: -->
Server=.\SQLEXPRESS;Database=CafeShopDB;Integrated Security=True;

<!-- If using LocalDB (Visual Studio default): -->
Server=(localdb)\MSSQLLocalDB;Database=CafeShopDB;Integrated Security=True;

<!-- If using full SQL Server on localhost: -->
Server=localhost;Database=CafeShopDB;Integrated Security=True;
```

To find your server name: in SQL Server Object Explorer, right-click the server → **Properties** → copy the "Name" field.

### Step 3 — Install NuGet Package (LiveCharts2)

Open **Tools → NuGet Package Manager → Package Manager Console** and run:

```powershell
Install-Package LiveChartsCore.SkiaSharpView.WinForms -ProjectName App.WindowsApp
```

### Step 4 — Build and Run

1. Right-click the solution → **Build Solution** (Ctrl+Shift+B)
2. Set `App.WindowsApp` as the **Startup Project** (right-click → Set as Startup Project)
3. Press **F5** to run

---

## 🖥️ Using the Application

| View | What you can do |
|---|---|
| **Dashboard** | See total orders, revenue, pending orders, customer count; view Bar chart (7-day revenue) and Pie chart (sales by category) |
| **Products** | Add/edit/view/delete products; search by name; filter by category; click column headers to sort |
| **Customers** | Add/edit/view/delete customers; search by name, phone, or email |
| **Orders** | Add/edit/view/delete orders; search by ID or customer name; filter by status (Pending/Preparing/Completed/Cancelled); rows color-coded by status |

### Adding an Order
1. Go to **Orders** → click **➕ Add**
2. Select a customer from the dropdown
3. In the "Order Items" section, select a product, set quantity, click **➕ Add Item**
4. Repeat for more items
5. Set status, add optional notes
6. Click **Save Order**

---

## 🗄️ Database Tables

```
Products  (ProductId PK, Name, Category, Price, StockQty, Description, IsAvailable, CreatedAt)
Customers (CustomerId PK, FullName, Phone, Email, Address, LoyaltyPts, CreatedAt)
Orders    (OrderId PK, CustomerId, CustomerName, TotalAmount, Status, Notes, OrderDate)
OrderItems(ItemId PK, OrderId, ProductId, ProductName, Quantity, UnitPrice, SubTotal [computed])
```

No foreign key constraints at the database level (as required by the project spec).
Referential integrity is enforced at the application/service layer.

---

## 👨‍🎓 Project Info

- **Subject:** Advanced Programming (COSC-5136)
- **Semester:** Spring 2026
- **Domain:** Cafe Shop Management System
- **Architecture:** App.Core + App.WindowsApp + SQL Server (ADO.NET)
- **Charts:** LiveChartsCore.SkiaSharpView.WinForms
