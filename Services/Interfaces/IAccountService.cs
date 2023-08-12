using AuctionApi.Models;

namespace AuctionApi.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GetJwt(LoginDto dto);
        List<UserDto> GetAll();
        UserDto GetUser(int id);
        void UpdateUser(int id, UpdateUserDto dto);
        void DeleteUser(int id);
    }
}
