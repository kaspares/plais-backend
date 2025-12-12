using Plais.DTOs.Auth;

namespace Plais.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDto login, HttpContext httpContext);
        Task LogoutAsync(HttpContext httpContext);
        UserDto? GetCurrentUser(HttpContext httpContext);
    }
}
