using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Models;

namespace App.Core.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll();
        Task<List<Product>> GetAllAsync();
        Product GetById(string productId);
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(string productId);
        List<Product> Search(string keyword, string category = "");
        List<string> GetCategories();
    }
}
