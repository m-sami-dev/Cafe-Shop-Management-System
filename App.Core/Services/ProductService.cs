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
    public class ProductService : IProductService
    {
        private readonly string _connectionString;

        public ProductService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CafeShopDB"].ConnectionString;
        }

        // ── Helper ──────────────────────────────────────────────────────────
        private Product MapReader(SqlDataReader r) => new Product
        {
            ProductId   = r["ProductId"].ToString(),
            Name        = r["Name"].ToString(),
            Category    = r["Category"].ToString(),
            Price       = Convert.ToDecimal(r["Price"]),
            StockQty    = Convert.ToInt32(r["StockQty"]),
            Description = r["Description"] == DBNull.Value ? "" : r["Description"].ToString(),
            IsAvailable = Convert.ToBoolean(r["IsAvailable"]),
            CreatedAt   = Convert.ToDateTime(r["CreatedAt"])
        };

        // ── GetAll ───────────────────────────────────────────────────────────
        public List<Product> GetAll()
        {
            var list = new List<Product>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT * FROM Products ORDER BY Category, Name", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapReader(r));
            }
            return list;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var list = new List<Product>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT * FROM Products ORDER BY Category, Name", conn))
            {
                await conn.OpenAsync();
                using (var r = await cmd.ExecuteReaderAsync())
                    while (await r.ReadAsync()) list.Add(MapReader(r));
            }
            return list;
        }

        // ── GetById ──────────────────────────────────────────────────────────
        public Product GetById(string productId)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT * FROM Products WHERE ProductId = @Id", conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 20) { Value = productId });
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    return r.Read() ? MapReader(r) : null;
            }
        }

        // ── Add ──────────────────────────────────────────────────────────────
        public bool Add(Product p)
        {
            const string sql = @"
                INSERT INTO Products (ProductId,Name,Category,Price,StockQty,Description,IsAvailable,CreatedAt)
                VALUES (@Id,@Name,@Category,@Price,@Stock,@Desc,@Avail,@Created)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id",      SqlDbType.NVarChar, 20)  { Value = p.ProductId });
                cmd.Parameters.Add(new SqlParameter("@Name",    SqlDbType.NVarChar, 100) { Value = p.Name });
                cmd.Parameters.Add(new SqlParameter("@Category",SqlDbType.NVarChar, 50)  { Value = p.Category });
                cmd.Parameters.Add(new SqlParameter("@Price",   SqlDbType.Decimal)        { Value = p.Price });
                cmd.Parameters.Add(new SqlParameter("@Stock",   SqlDbType.Int)            { Value = p.StockQty });
                cmd.Parameters.Add(new SqlParameter("@Desc",    SqlDbType.NVarChar, 300) { Value = (object)p.Description ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Avail",   SqlDbType.Bit)            { Value = p.IsAvailable });
                cmd.Parameters.Add(new SqlParameter("@Created", SqlDbType.DateTime)       { Value = p.CreatedAt });
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ── Update ───────────────────────────────────────────────────────────
        public bool Update(Product p)
        {
            const string sql = @"
                UPDATE Products
                SET Name=@Name, Category=@Category, Price=@Price,
                    StockQty=@Stock, Description=@Desc, IsAvailable=@Avail
                WHERE ProductId=@Id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id",      SqlDbType.NVarChar, 20)  { Value = p.ProductId });
                cmd.Parameters.Add(new SqlParameter("@Name",    SqlDbType.NVarChar, 100) { Value = p.Name });
                cmd.Parameters.Add(new SqlParameter("@Category",SqlDbType.NVarChar, 50)  { Value = p.Category });
                cmd.Parameters.Add(new SqlParameter("@Price",   SqlDbType.Decimal)        { Value = p.Price });
                cmd.Parameters.Add(new SqlParameter("@Stock",   SqlDbType.Int)            { Value = p.StockQty });
                cmd.Parameters.Add(new SqlParameter("@Desc",    SqlDbType.NVarChar, 300) { Value = (object)p.Description ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Avail",   SqlDbType.Bit)            { Value = p.IsAvailable });
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ── Delete ───────────────────────────────────────────────────────────
        public bool Delete(string productId)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("DELETE FROM Products WHERE ProductId=@Id", conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 20) { Value = productId });
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ── Search ───────────────────────────────────────────────────────────
        public List<Product> Search(string keyword, string category = "")
        {
            var list = new List<Product>();
            var sql  = "SELECT * FROM Products WHERE (Name LIKE @kw OR Description LIKE @kw)";
            if (!string.IsNullOrWhiteSpace(category)) sql += " AND Category=@cat";
            sql += " ORDER BY Name";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@kw", SqlDbType.NVarChar, 102) { Value = $"%{keyword}%" });
                if (!string.IsNullOrWhiteSpace(category))
                    cmd.Parameters.Add(new SqlParameter("@cat", SqlDbType.NVarChar, 50) { Value = category });
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapReader(r));
            }
            return list;
        }

        // ── GetCategories ────────────────────────────────────────────────────
        public List<string> GetCategories()
        {
            var cats = new List<string>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT DISTINCT Category FROM Products ORDER BY Category", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) cats.Add(r[0].ToString());
            }
            return cats;
        }
    }
}
