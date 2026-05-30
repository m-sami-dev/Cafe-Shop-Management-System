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
    public class CustomerService : ICustomerService
    {
        private readonly string _connectionString;

        public CustomerService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CafeShopDB"].ConnectionString;
        }

        private Customer MapReader(SqlDataReader r) => new Customer
        {
            CustomerId = r["CustomerId"].ToString(),
            FullName   = r["FullName"].ToString(),
            Phone      = r["Phone"].ToString(),
            Email      = r["Email"] == DBNull.Value ? "" : r["Email"].ToString(),
            Address    = r["Address"] == DBNull.Value ? "" : r["Address"].ToString(),
            LoyaltyPts = Convert.ToInt32(r["LoyaltyPts"]),
            CreatedAt  = Convert.ToDateTime(r["CreatedAt"])
        };

        public List<Customer> GetAll()
        {
            var list = new List<Customer>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT * FROM Customers ORDER BY FullName", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapReader(r));
            }
            return list;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var list = new List<Customer>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT * FROM Customers ORDER BY FullName", conn))
            {
                await conn.OpenAsync();
                using (var r = await cmd.ExecuteReaderAsync())
                    while (await r.ReadAsync()) list.Add(MapReader(r));
            }
            return list;
        }

        public Customer GetById(string customerId)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("SELECT * FROM Customers WHERE CustomerId=@Id", conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 20) { Value = customerId });
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    return r.Read() ? MapReader(r) : null;
            }
        }

        public bool Add(Customer c)
        {
            const string sql = @"
                INSERT INTO Customers (CustomerId,FullName,Phone,Email,Address,LoyaltyPts,CreatedAt)
                VALUES (@Id,@Name,@Phone,@Email,@Addr,@Pts,@Created)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id",      SqlDbType.NVarChar, 20)  { Value = c.CustomerId });
                cmd.Parameters.Add(new SqlParameter("@Name",    SqlDbType.NVarChar, 100) { Value = c.FullName });
                cmd.Parameters.Add(new SqlParameter("@Phone",   SqlDbType.NVarChar, 20)  { Value = c.Phone });
                cmd.Parameters.Add(new SqlParameter("@Email",   SqlDbType.NVarChar, 100) { Value = (object)c.Email ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Addr",    SqlDbType.NVarChar, 300) { Value = (object)c.Address ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Pts",     SqlDbType.Int)            { Value = c.LoyaltyPts });
                cmd.Parameters.Add(new SqlParameter("@Created", SqlDbType.DateTime)       { Value = c.CreatedAt });
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Customer c)
        {
            const string sql = @"
                UPDATE Customers
                SET FullName=@Name, Phone=@Phone, Email=@Email, Address=@Addr, LoyaltyPts=@Pts
                WHERE CustomerId=@Id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id",    SqlDbType.NVarChar, 20)  { Value = c.CustomerId });
                cmd.Parameters.Add(new SqlParameter("@Name",  SqlDbType.NVarChar, 100) { Value = c.FullName });
                cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 20)  { Value = c.Phone });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object)c.Email ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Addr",  SqlDbType.NVarChar, 300) { Value = (object)c.Address ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Pts",   SqlDbType.Int)            { Value = c.LoyaltyPts });
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string customerId)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand("DELETE FROM Customers WHERE CustomerId=@Id", conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 20) { Value = customerId });
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Customer> Search(string keyword)
        {
            var list = new List<Customer>();
            const string sql = @"
                SELECT * FROM Customers
                WHERE FullName LIKE @kw OR Phone LIKE @kw OR Email LIKE @kw
                ORDER BY FullName";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd  = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@kw", SqlDbType.NVarChar, 102) { Value = $"%{keyword}%" });
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapReader(r));
            }
            return list;
        }
    }
}
