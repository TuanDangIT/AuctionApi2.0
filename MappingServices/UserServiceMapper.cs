using AuctionApi.Models;
using Microsoft.AspNetCore.Identity;

namespace AuctionApi.MappingServices
{
    public class UserServiceMapper
    {
        public static User MapToUser(RegisterUserDto userDto)
        {
            var newUser = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                RoleId = userDto.RoleId
                
            };
            
            return newUser;
        }
    }
}