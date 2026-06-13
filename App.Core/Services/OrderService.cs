using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using App.Core.Interfaces;
using App.Core.Models;

namespace App.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly string _connectionString;

        public OrderService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CafeShopDB"].ConnectionString;
        }

        private Order MapOrderReader(SqlDataReader r) => new Order
        {
            OrderId      = r["OrderId"].ToString(),
            CustomerId   = r["CustomerId"].ToString(),
            CustomerName = r["CustomerName"].ToString(),
            TotalAmount  = Convert.ToDecimal(r["TotalAmount"]),
            Status       = r["Status"].ToString(),
            Notes        = r["Notes"] == DBNull.Value ? "" : r["Notes"].ToString(),
            OrderDate    = Convert.ToDateTime(r["OrderDate"])
        };

        private OrderItem MapItemReader(SqlDataReader r) => new OrderItem
        {
            ItemId      = r["ItemId"].ToString(),
            OrderId     = r["OrderId"].ToString(),
            ProductId   = r["ProductId"].ToString(),
            ProductName = r["ProductName"].ToString(),
            Quantity    = Convert.ToInt32(r["Quantity"]),
            UnitPrice   = Convert.ToDecimal(r["UnitPrice"])
        };

        // ── GetAll ───────────────────────────────────────────────────────────
        public List<Order> GetAll()
        {
            var list = new List<Order>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT * FROM Orders ORDER BY OrderDate DESC", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapOrderReader(r));
            }
            return list;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var list = new List<Order>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT * FROM Orders ORDER BY OrderDate DESC", conn))
            {
                await conn.OpenAsync();
                using (var r = await cmd.ExecuteReaderAsync())
                    while (await r.ReadAsync()) list.Add(MapOrderReader(r));
            }
            return list;
        }

        // ── GetById (includes items) ─────────────────────────────────────────
        public Order GetById(string orderId)
        {
            Order order = null;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT * FROM Orders WHERE OrderId=@Id", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 20) { Value = orderId });
                    using (var r = cmd.ExecuteReader())
                        if (r.Read()) order = MapOrderReader(r);
                }

                if (order == null) return null;

                using (var cmd = new SqlCommand("SELECT * FROM OrderItems WHERE OrderId=@Id", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 20) { Value = orderId });
                    using (var r = cmd.ExecuteReader())
                        while (r.Read()) order.Items.Add(MapItemReader(r));
                }
            }
            return order;
        }

        // ── Add ──────────────────────────────────────────────────────────────
        public bool Add(Order o)
        {
            const string orderSql = @"
                INSERT INTO Orders (OrderId,CustomerId,CustomerName,TotalAmount,Status,Notes,OrderDate)
                VALUES (@Id,@CusId,@CusName,@Total,@Status,@Notes,@Date)";

            const string itemSql = @"
                INSERT INTO OrderItems (ItemId,OrderId,ProductId,ProductName,Quantity,UnitPrice)
                VALUES (@ItemId,@OrdId,@ProdId,@ProdName,@Qty,@Price)";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(orderSql, conn, tx))
                        {
                            cmd.Parameters.Add(new SqlParameter("@Id",      SqlDbType.NVarChar, 20)  { Value = o.OrderId });
                            cmd.Parameters.Add(new SqlParameter("@CusId",   SqlDbType.NVarChar, 20)  { Value = o.CustomerId });
                            cmd.Parameters.Add(new SqlParameter("@CusName", SqlDbType.NVarChar, 100) { Value = o.CustomerName });
                            cmd.Parameters.Add(new SqlParameter("@Total",   SqlDbType.Decimal)        { Value = o.TotalAmount });
                            cmd.Parameters.Add(new SqlParameter("@Status",  SqlDbType.NVarChar, 20)  { Value = o.Status });
                            cmd.Parameters.Add(new SqlParameter("@Notes",   SqlDbType.NVarChar, 300) { Value = (object)o.Notes ?? DBNull.Value });
                            cmd.Parameters.Add(new SqlParameter("@Date",    SqlDbType.DateTime)       { Value = o.OrderDate });
                            cmd.ExecuteNonQuery();
                        }

                        foreach (var item in o.Items)
                        {
                            using (var cmd = new SqlCommand(itemSql, conn, tx))
                            {
                                cmd.Parameters.Add(new SqlParameter("@ItemId",   SqlDbType.NVarChar, 20)  { Value = item.ItemId });
                                cmd.Parameters.Add(new SqlParameter("@OrdId",    SqlDbType.NVarChar, 20)  { Value = o.OrderId });
                                cmd.Parameters.Add(new SqlParameter("@ProdId",   SqlDbType.NVarChar, 20)  { Value = item.ProductId });
                                cmd.Parameters.Add(new SqlParameter("@ProdName", SqlDbType.NVarChar, 100) { Value = item.ProductName });
                                cmd.Parameters.Add(new SqlParameter("@Qty",      SqlDbType.Int)            { Value = item.Quantity });
                                cmd.Parameters.Add(new SqlParameter("@Price",    SqlDbType.Decimal)        { Value = item.UnitPrice });
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch
                    {
                        tx.Rollback();
                        return false;
                    }
                }
            }
        }

        // ── Update ───────────────────────────────────────────────────────────
        public bool Update(Order o)
        {
            const string sql = @"
                UPDATE Orders
                SET CustomerId=@CusId, CustomerName=@CusName, TotalAmount=@Total,
                    Status=@Status, Notes=@Notes
                WHERE OrderId=@Id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id",      SqlDbType.NVarChar, 20)  { Value = o.OrderId });
                cmd.Parameters.Add(new SqlParameter("@CusId",   SqlDbType.NVarChar, 20)  { Value = o.CustomerId });
                cmd.Parameters.Add(new SqlParameter("@CusName", SqlDbType.NVarChar, 100) { Value = o.CustomerName });
                cmd.Parameters.Add(new SqlParameter("@Total",   SqlDbType.Decimal)        { Value = o.TotalAmount });
                cmd.Parameters.Add(new SqlParameter("@Status",  SqlDbType.NVarChar, 20)  { Value = o.Status });
                cmd.Parameters.Add(new SqlParameter("@Notes",   SqlDbType.NVarChar, 300) { Value = (object)o.Notes ?? DBNull.Value });
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ── Delete ───────────────────────────────────────────────────────────
        public bool Delete(string orderId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DELETE FROM OrderItems WHERE OrderId=@Id", conn, tx))
                        {
                            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 20) { Value = orderId });
                            cmd.ExecuteNonQuery();
                        }
                        using (var cmd = new SqlCommand("DELETE FROM Orders WHERE OrderId=@Id", conn, tx))
                        {
                            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 20) { Value = orderId });
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        return true;
                    }
                    catch { tx.Rollback(); return false; }
                }
            }
        }

        // ── Search ───────────────────────────────────────────────────────────
        //public List<Order> Search(string keyword, string status = "")
        //{
        //    var list = new List<Order>();
        //    var sql  = "SELECT * FROM Orders WHERE (CustomerName LIKE @kw OR OrderId LIKE @kw)";
        //    if (!string.IsNullOrWhiteSpace(status)) sql += " AND Status=@status";
        //    sql += " ORDER BY OrderDate DESC";

        //    using (var conn = new SqlConnection(_connectionString))
        //    using (var cmd  = new SqlCommand(sql, conn))
        //    {
        //        cmd.Parameters.Add(new SqlParameter("@kw", SqlDbType.NVarChar, 102) { Value = $"%{keyword}%" });
        //        if (!string.IsNullOrWhiteSpace(status))
        //            cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.NVarChar, 20) { Value = status });
        //        conn.Open();
        //        using (var r = cmd.ExecuteReader())
        //            while (r.Read()) list.Add(MapOrderReader(r));
        //    }
        //    return list;
        //}
        
        public List<Order> Search(string keyword, string status = "")
        {
            var list = new List<Order>();

            bool hasKeyword = !string.IsNullOrWhiteSpace(keyword);
            bool hasStatus = !string.IsNullOrWhiteSpace(status);

            var sql = "SELECT * FROM Orders WHERE 1=1";
            if (hasKeyword) sql += " AND (CustomerName LIKE @kw OR OrderId LIKE @kw)";
            if (hasStatus) sql += " AND Status = @status";
            sql += " ORDER BY OrderDate DESC";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (hasKeyword)
                    cmd.Parameters.Add(new SqlParameter("@kw", SqlDbType.NVarChar, 102)
                    { Value = $"%{keyword}%" });
                if (hasStatus)
                    cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.NVarChar, 20)
                    { Value = status });

                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapOrderReader(r));
            }
            return list;
        }

        // ── Chart: Sales by Category ─────────────────────────────────────────
        public Dictionary<string, decimal> GetSalesByCategory()
        {
            var result = new Dictionary<string, decimal>();
            const string sql = @"
                SELECT p.Category, SUM(oi.Quantity * oi.UnitPrice) AS Total
                FROM OrderItems oi
                JOIN Products p ON p.ProductId = oi.ProductId
                JOIN Orders o ON o.OrderId = oi.OrderId
                WHERE o.Status = 'Completed'
                GROUP BY p.Category
                ORDER BY Total DESC";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        result[r["Category"].ToString()] = Convert.ToDecimal(r["Total"]);
            }
            return result;
        }

        // ── Chart: Daily Revenue ─────────────────────────────────────────────
        public Dictionary<string, decimal> GetDailyRevenue(int days = 7)
        {
            var result = new Dictionary<string, decimal>();
            const string sql = @"
                SELECT CONVERT(NVARCHAR(10), OrderDate, 120) AS Day,
                       SUM(TotalAmount) AS Revenue
                FROM Orders
                WHERE OrderDate >= DATEADD(day, @Days, GETDATE())
                  AND Status = 'Completed'
                GROUP BY CONVERT(NVARCHAR(10), OrderDate, 120)
                ORDER BY Day";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Days", SqlDbType.Int) { Value = -days });
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        result[r["Day"].ToString()] = Convert.ToDecimal(r["Revenue"]);
            }
            return result;
        }

        // ── Stats ─────────────────────────────────────────────────────────────
        public int GetTotalOrders()
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT COUNT(*) FROM Orders", conn))
            {
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public decimal GetTotalRevenue()
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT ISNULL(SUM(TotalAmount),0) FROM Orders WHERE Status='Completed'", conn))
            {
                conn.Open();
                return (decimal)cmd.ExecuteScalar();
            }
        }

        public int GetPendingOrdersCount()
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE Status IN ('Pending','Preparing')", conn))
            {
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
