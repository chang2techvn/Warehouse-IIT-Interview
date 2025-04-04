using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Models.DTOs;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            // Check if user exists
            if (await _authService.UserExistsAsync(registerDto.Username))
                return BadRequest("Username already exists");
                
            var userToReturn = await _authService.RegisterAsync(registerDto);
            
            return Ok(userToReturn);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userFromRepo = await _authService.LoginAsync(loginDto);
            
            if (userFromRepo == null)
                return Unauthorized();
                
            return Ok(userFromRepo);
        }
    }
}