using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.Auth;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var result = await authService.LoginAsync(login, HttpContext);
            
            if(!result)
            {
                return Unauthorized();
            }

            return Ok();

        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync(HttpContext);
            return Ok();
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<UserDto> Me()
        {
            var user = authService.GetCurrentUser(HttpContext);
            
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
    }
}
