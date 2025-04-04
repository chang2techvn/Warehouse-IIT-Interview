using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseAPI.Data.Repositories;
using WarehouseAPI.Models;
using WarehouseAPI.Models.DTOs;

namespace WarehouseAPI.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        
        public StockService(IProductRepository productRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }
        
        public async Task<bool> IncreaseStockAsync(StockMovementDto movementDto, int userId)
        {
            var product = await _productRepository.GetProductByIdAsync(movementDto.ProductId);
            
            if (product == null)
                return false;
                
            // Create stock movement
            var movement = new StockMovement
            {
                ProductId = movementDto.ProductId,
                Quantity = movementDto.Quantity,
                Type = MovementType.StockIn,
                Notes = movementDto.Notes,
                UserId = userId
            };
            
            await _stockRepository.AddMovementAsync(movement);
            
            // Update product stock
            product.CurrentStock += movementDto.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            
            return await _productRepository.UpdateProductAsync(product);
        }
        
        public async Task<bool> DecreaseStockAsync(StockMovementDto movementDto, int userId)
        {
            var product = await _productRepository.GetProductByIdAsync(movementDto.ProductId);
            
            if (product == null)
                return false;
                
            // Check if there's enough stock
            if (product.CurrentStock < movementDto.Quantity)
                return false;
                
            // Create stock movement
            var movement = new StockMovement
            {
                ProductId = movementDto.ProductId,
                Quantity = movementDto.Quantity,
                Type = MovementType.StockOut,
                Notes = movementDto.Notes,
                UserId = userId
            };
            
            await _stockRepository.AddMovementAsync(movement);
            
            // Update product stock
            product.CurrentStock -= movementDto.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            
            return await _productRepository.UpdateProductAsync(product);
        }
        
        public async Task<IEnumerable<StockMovementHistoryDto>> GetStockHistoryAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            
            if (product == null)
                return null;
                
            var movements = await _stockRepository.GetMovementsByProductIdAsync(productId);
            
            return movements.Select(m => new StockMovementHistoryDto
            {
                Id = m.Id,
                ProductId = m.ProductId,
                ProductName = product.Name,
                Quantity = m.Quantity,
                MovementType = m.Type.ToString(),
                Notes = m.Notes,
                Username = m.User.Username,
                CreatedAt = m.CreatedAt
            });
        }
    }
}