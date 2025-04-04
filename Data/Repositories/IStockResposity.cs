using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAPI.Models;

namespace WarehouseAPI.Data.Repositories
{
    public interface IStockRepository
    {
        Task<StockMovement> AddMovementAsync(StockMovement movement);
        Task<IEnumerable<StockMovement>> GetMovementsByProductIdAsync(int productId);
    }
}