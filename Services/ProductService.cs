using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseAPI.Data.Repositories;
using WarehouseAPI.Models;
using WarehouseAPI.Models.DTOs;

namespace WarehouseAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        
        public ProductService(IProductRepository productRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }
        
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                SKU = p.SKU,
                CurrentStock = p.CurrentStock,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }
        
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            
            if (product == null)
                return null;
                
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                CurrentStock = product.CurrentStock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
        
        public async Task<ProductDto> AddProductAsync(CreateProductDto productDto, int userId)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                SKU = productDto.SKU,
                CurrentStock = productDto.InitialStock
            };
            
            await _productRepository.AddProductAsync(product);
            
            // If initial stock is greater than 0, create a stock movement
            if (productDto.InitialStock > 0)
            {
                var movement = new StockMovement
                {
                    ProductId = product.Id,
                    Quantity = productDto.InitialStock,
                    Type = MovementType.StockIn,
                    Notes = "Initial stock",
                    UserId = userId
                };
                
                await _stockRepository.AddMovementAsync(movement);
            }
            
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                CurrentStock = product.CurrentStock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}