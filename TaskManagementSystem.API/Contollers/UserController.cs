using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Services;

namespace TaskManagementSystem.API.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var result = await _userService.RegisterAsync(userDTO);
            if (result == null)
            {
                return BadRequest("Username already exists");
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var token = await _userService.LoginAsync(userDTO);
            if(string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Invalid credentials!" });
            }
            return Ok(new { token });
        }
    }
}
