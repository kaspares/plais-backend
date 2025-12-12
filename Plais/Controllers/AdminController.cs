using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plais.DTOs.Auth;
using Plais.Services;
using Plais.Services.Interfaces;

namespace Plais.Controllers
{
    [Route("/api/admins")]
    [ApiController]
    [Authorize]

    public class AdminController(IAdminService adminService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await adminService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            await adminService.CreateUserAsync(dto);
            return Created();
        }

        [HttpPut("{userName}/reset-password")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto, [FromRoute] string userName)
        {
            await adminService.ResetPasswordAsync(userName, dto.NewPassword);

            return NoContent();
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userName)
        {
            await adminService.DeleteUserAsync(userName);
            return NoContent();
        }

    }
}
