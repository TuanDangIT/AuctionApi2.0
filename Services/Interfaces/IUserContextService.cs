using System.Security.Claims;

namespace AuctionApi.Services.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int ? GetUserId { get; }
    }
}
