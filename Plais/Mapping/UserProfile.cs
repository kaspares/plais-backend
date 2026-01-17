using AutoMapper;
using Plais.DTOs.Auth;
using Plais.Models;

namespace Plais.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
