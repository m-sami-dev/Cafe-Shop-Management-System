using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Models;

namespace App.Core.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Task<List<Order>> GetAllAsync();
        Order GetById(string orderId);
        bool Add(Order order);
        bool Update(Order order);
        bool Delete(string orderId);
        List<Order> Search(string keyword, string status = "");
        Dictionary<string, decimal> GetSalesByCategory();
        Dictionary<string, decimal> GetDailyRevenue(int days = 7);
        int GetTotalOrders();
        decimal GetTotalRevenue();
        int GetPendingOrdersCount();
    }
}
