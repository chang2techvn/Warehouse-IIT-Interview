using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAPI.Models;

namespace WarehouseAPI.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductWithMovementsAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> ProductExistsAsync(int id);
    }
}
