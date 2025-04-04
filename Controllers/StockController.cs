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
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        
        [HttpPost("increase")]
        public async Task<IActionResult> IncreaseStock(StockMovementDto movementDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var result = await _stockService.IncreaseStockAsync(movementDto, userId);
            
            if (!result)
                return BadRequest("Failed to increase stock");
                
            return Ok(new { message = "Stock increased successfully" });
        }
        
        [HttpPost("decrease")]
        public async Task<IActionResult> DecreaseStock(StockMovementDto movementDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var result = await _stockService.DecreaseStockAsync(movementDto, userId);
            
            if (!result)
                return BadRequest("Failed to decrease stock. Check if there's enough stock available.");
                
            return Ok(new { message = "Stock decreased successfully" });
        }
    }
}