using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Models;

namespace WarehouseAPI.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<StockMovement> AddMovementAsync(StockMovement movement)
        {
            await _context.StockMovements.AddAsync(movement);
            await _context.SaveChangesAsync();
            return movement;
        }
        
        public async Task<IEnumerable<StockMovement>> GetMovementsByProductIdAsync(int productId)
        {
            return await _context.StockMovements
                .Include(sm => sm.User)
                .Where(sm => sm.ProductId == productId)
                .OrderByDescending(sm => sm.CreatedAt)
                .ToListAsync();
        }
    }
}