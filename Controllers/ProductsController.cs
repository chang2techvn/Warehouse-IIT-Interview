using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Models.DTOs;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStockService _stockService;
        
        public ProductsController(IProductService productService, IStockService stockService)
        {
            _productService = productService;
            _stockService = stockService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            
            if (product == null)
                return NotFound();
                
            return Ok(product);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var createdProduct = await _productService.AddProductAsync(productDto, userId);
            
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }
        
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetProductHistory(int id)
        {
            var history = await _stockService.GetStockHistoryAsync(id);
            
            if (history == null)
                return NotFound();
                
            return Ok(history);
        }
    }
}