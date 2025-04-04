using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseAPI.Models.DTOs;

namespace WarehouseAPI.Services
{
    public interface IStockService
    {
        Task<bool> IncreaseStockAsync(StockMovementDto movementDto, int userId);
        Task<bool> DecreaseStockAsync(StockMovementDto movementDto, int userId);
        Task<IEnumerable<StockMovementHistoryDto>> GetStockHistoryAsync(int productId);
    }
}