-- =============================================
-- Cafe Shop Management System Database
-- COSC-5136 Advanced Programming | Spring 2026
-- =============================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'CafeShopDB')
    DROP DATABASE CafeShopDB;
GO

CREATE DATABASE CafeShopDB;
GO

USE CafeShopDB;
GO

-- =============================================
-- TABLE: Products
-- =============================================
CREATE TABLE Products (
    ProductId   NVARCHAR(20)   NOT NULL PRIMARY KEY,
    Name        NVARCHAR(100)  NOT NULL,
    Category    NVARCHAR(50)   NOT NULL,
    Price       DECIMAL(10,2)  NOT NULL,
    StockQty    INT            NOT NULL DEFAULT 0,
    Description NVARCHAR(300)  NULL,
    IsAvailable BIT            NOT NULL DEFAULT 1,
    CreatedAt   DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- =============================================
-- TABLE: Customers
-- =============================================
CREATE TABLE Customers (
    CustomerId  NVARCHAR(20)   NOT NULL PRIMARY KEY,
    FullName    NVARCHAR(100)  NOT NULL,
    Phone       NVARCHAR(20)   NOT NULL,
    Email       NVARCHAR(100)  NULL,
    Address     NVARCHAR(300)  NULL,
    LoyaltyPts  INT            NOT NULL DEFAULT 0,
    CreatedAt   DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- =============================================
-- TABLE: Orders
-- =============================================
CREATE TABLE Orders (
    OrderId      NVARCHAR(20)   NOT NULL PRIMARY KEY,
    CustomerId   NVARCHAR(20)   NOT NULL,
    CustomerName NVARCHAR(100)  NOT NULL,
    TotalAmount  DECIMAL(10,2)  NOT NULL DEFAULT 0,
    Status       NVARCHAR(20)   NOT NULL DEFAULT 'Pending',
    Notes        NVARCHAR(300)  NULL,
    OrderDate    DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- =============================================
-- TABLE: OrderItems
-- =============================================
CREATE TABLE OrderItems (
    ItemId      NVARCHAR(20)   NOT NULL PRIMARY KEY,
    OrderId     NVARCHAR(20)   NOT NULL,
    ProductId   NVARCHAR(20)   NOT NULL,
    ProductName NVARCHAR(100)  NOT NULL,
    Quantity    INT            NOT NULL,
    UnitPrice   DECIMAL(10,2)  NOT NULL,
    SubTotal    AS (Quantity * UnitPrice) PERSISTED
);
GO

-- =============================================
-- SEED DATA: Products
-- =============================================
INSERT INTO Products (ProductId, Name, Category, Price, StockQty, Description, IsAvailable) VALUES
('PRD-001', 'Espresso',          'Coffee',    250.00, 100, 'Strong single shot espresso',        1),
('PRD-002', 'Cappuccino',        'Coffee',    350.00, 100, 'Espresso with steamed milk foam',     1),
('PRD-003', 'Latte',             'Coffee',    380.00, 100, 'Espresso with lots of steamed milk',  1),
('PRD-004', 'Americano',         'Coffee',    300.00, 100, 'Espresso diluted with hot water',     1),
('PRD-005', 'Green Tea',         'Tea',       200.00, 80,  'Fresh green tea leaves',              1),
('PRD-006', 'Kashmiri Tea',      'Tea',       220.00, 80,  'Traditional pink Kashmiri chai',      1),
('PRD-007', 'Chocolate Cake',    'Bakery',    450.00, 30,  'Rich moist chocolate layer cake',     1),
('PRD-008', 'Croissant',         'Bakery',    280.00, 40,  'Buttery flaky French croissant',      1),
('PRD-009', 'Club Sandwich',     'Food',      550.00, 20,  'Triple decker club sandwich',         1),
('PRD-010', 'Cold Coffee',       'Coffee',    400.00, 60,  'Iced blended cold coffee',            1);
GO

-- =============================================
-- SEED DATA: Customers
-- =============================================
INSERT INTO Customers (CustomerId, FullName, Phone, Email, Address, LoyaltyPts) VALUES
('CUS-001', 'Ahmed Raza',    '0300-1234567', 'ahmed@email.com',   'Lahore, Punjab',      150),
('CUS-002', 'Sara Khan',     '0311-2345678', 'sara@email.com',    'Karachi, Sindh',      80),
('CUS-003', 'Ali Hassan',    '0321-3456789', 'ali@email.com',     'Islamabad',           220),
('CUS-004', 'Fatima Malik',  '0333-4567890', 'fatima@email.com',  'Faisalabad, Punjab',  50),
('CUS-005', 'Usman Tariq',   '0345-5678901', NULL,                'Multan, Punjab',      310);
GO

-- =============================================
-- SEED DATA: Orders
-- =============================================
INSERT INTO Orders (OrderId, CustomerId, CustomerName, TotalAmount, Status, OrderDate) VALUES
('ORD-001', 'CUS-001', 'Ahmed Raza',   980.00,  'Completed', DATEADD(day,-5, GETDATE())),
('ORD-002', 'CUS-002', 'Sara Khan',    630.00,  'Completed', DATEADD(day,-4, GETDATE())),
('ORD-003', 'CUS-003', 'Ali Hassan',   1380.00, 'Completed', DATEADD(day,-3, GETDATE())),
('ORD-004', 'CUS-001', 'Ahmed Raza',   750.00,  'Completed', DATEADD(day,-2, GETDATE())),
('ORD-005', 'CUS-004', 'Fatima Malik', 450.00,  'Pending',   DATEADD(day,-1, GETDATE())),
('ORD-006', 'CUS-005', 'Usman Tariq',  1600.00, 'Completed', GETDATE()),
('ORD-007', 'CUS-003', 'Ali Hassan',   680.00,  'Preparing', GETDATE());
GO

INSERT INTO OrderItems (ItemId, OrderId, ProductId, ProductName, Quantity, UnitPrice) VALUES
('ITM-001', 'ORD-001', 'PRD-002', 'Cappuccino',     2, 350.00),
('ITM-002', 'ORD-001', 'PRD-008', 'Croissant',      1, 280.00),
('ITM-003', 'ORD-002', 'PRD-001', 'Espresso',       1, 250.00),
('ITM-004', 'ORD-002', 'PRD-007', 'Chocolate Cake', 1, 450.00), -- note: 250+450=700 approx
('ITM-005', 'ORD-003', 'PRD-003', 'Latte',          2, 380.00),
('ITM-006', 'ORD-003', 'PRD-009', 'Club Sandwich',  1, 550.00),
('ITM-007', 'ORD-004', 'PRD-010', 'Cold Coffee',    1, 400.00),
('ITM-008', 'ORD-004', 'PRD-006', 'Kashmiri Tea',   1, 220.00),
('ITM-009', 'ORD-005', 'PRD-007', 'Chocolate Cake', 1, 450.00),
('ITM-010', 'ORD-006', 'PRD-009', 'Club Sandwich',  2, 550.00),
('ITM-011', 'ORD-006', 'PRD-002', 'Cappuccino',     1, 350.00),
('ITM-012', 'ORD-007', 'PRD-003', 'Latte',          1, 380.00),
('ITM-013', 'ORD-007', 'PRD-008', 'Croissant',      1, 280.00);
GO

PRINT 'CafeShopDB created and seeded successfully.';
GO
