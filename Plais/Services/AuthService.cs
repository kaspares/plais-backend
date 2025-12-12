using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Plais.DTOs.Auth;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;
using PLAIS.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Plais.Services
{
    public class AuthService(SignInManager<User> signInManager,
        UserManager<User> userManager,
        IMapper mapper) : IAuthService
    {
        public async Task ResetPasswordAsync(string userName, string newPassword)
        {
            
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
               {
                   throw new PasswordChangeFailedException();
               }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var result = await userManager.ResetPasswordAsync(user, token , newPassword);
                if (!result.Succeeded)
                {
                    throw new PasswordChangeFailedException();
                }
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await userManager.FindByNameAsync(dto.UserName);
            if (existingUser != null)
            {
                throw new NotImplementedException("User already exists.");
            }

            var user = new User
            {
                UserName = dto.UserName
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if(!result.Succeeded)
            {
                throw new NotImplementedException("Failed to create user");
            }

            await userManager.AddToRoleAsync(user, "Admin");
            
        }

        public async Task DeleteUserAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if(user == null)
            {
                throw new NotFoundException(nameof(User), userName);
            }

            var result = await userManager.DeleteAsync(user);
            if(!result.Succeeded)
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

        public UserDto? GetCurrentUser(HttpContext httpContext)
        {
            var user = httpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            return new UserDto
            {
                Username = user.Identity.Name!
            };
        }

        public async Task<bool> LoginAsync(LoginDto login, HttpContext httpContext)
        {
            var user = await userManager.FindByNameAsync(login.Username);
            if(user == null || !await userManager.CheckPasswordAsync(user, login.Password))
            {
                return false;
            }

            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            };

            await httpContext.SignInAsync(
                IdentityConstants.ApplicationScheme,
                await signInManager.CreateUserPrincipalAsync(user), props);

            return true;
        }

        public async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        }
    }
}
