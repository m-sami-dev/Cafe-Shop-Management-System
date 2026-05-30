using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Models;

namespace App.Core.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Task<List<Customer>> GetAllAsync();
        Customer GetById(string customerId);
        bool Add(Customer customer);
        bool Update(Customer customer);
        bool Delete(string customerId);
        List<Customer> Search(string keyword);
    }
}
