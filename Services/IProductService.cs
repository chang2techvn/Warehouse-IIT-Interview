using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAPI.Models.DTOs;

namespace WarehouseAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> AddProductAsync(CreateProductDto productDto, int userId);
    }
}