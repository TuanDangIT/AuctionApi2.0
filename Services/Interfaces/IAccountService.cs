using AuctionApi.Models;

namespace AuctionApi.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GetJwt(LoginDto dto);
        List<UserDto> GetAll();
        UserDto GetById(int id);
        void UpdateUser(int id, UpdateUserDto dto);
    }
}
