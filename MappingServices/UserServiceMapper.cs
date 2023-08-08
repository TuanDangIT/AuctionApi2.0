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

        public static UserDto MapToUserDto(User user)
        {
            var auctions = user.Auctions;
            var auctionTitleList = new List<string>();
            foreach (var auction in auctions)
            {
                auctionTitleList.Add(auction.Title);
            }
            var newUserDto = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Balance = user.Balance,
                Auctions = auctionTitleList
            };
            return newUserDto;
        }
    }
}