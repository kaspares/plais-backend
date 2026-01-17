using Plais.DTOs.Auth;

namespace Plais.Services.Interfaces
{
    public interface IAdminService
    {
        Task CreateUserAsync(CreateUserDto dto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task DeleteUserAsync(string userName);
        Task ResetPasswordAsync(string userName, string newPassword);
    }
}
