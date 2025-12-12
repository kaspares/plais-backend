using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Plais.DTOs.Auth;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Plais.Services
{
    public class AdminService(UserManager<User> userManager,
        IMapper mapper): IAdminService
    {
        public async Task CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await userManager.FindByNameAsync(dto.UserName);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            var user = new User
            {
                UserName = dto.UserName
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Failed to create user");
            }

        }

        public async Task DeleteUserAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userName);
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to delete user");
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var result = mapper.Map<IEnumerable<UserDto>>(users);

            return result;
        }

        public async Task ResetPasswordAsync(string userName, string newPassword)
        {

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new PasswordChangeFailedException();
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new PasswordChangeFailedException();
            }
        }
    }
}
