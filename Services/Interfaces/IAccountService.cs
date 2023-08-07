using AuctionApi.Models;

namespace AuctionApi.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GetJwt(LoginDto dto);
    }
}
