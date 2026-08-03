using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions
{
    public static class AppUserExtensions
    {
        public static UserDTO ToUserDto(this AppUser appUser)
        {
            return new UserDTO
            {
                Id = appUser.Id,
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                ImageUrl = ""
            };
        }

        public static UserAuthDTO ToUserAuthDto(this AppUser appUser, ITokenService tokenService)
        {
            return new UserAuthDTO
            {
                Id = appUser.Id,
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                ImageUrl = "",
                Token = tokenService.CreateToken(appUser)
            };
        }
    }
}