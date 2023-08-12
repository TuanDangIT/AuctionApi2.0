using AuctionApi.Authorization;
using AuctionApi.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace AuctionApi.Services.Utils
{
    public class CheckAuthorizationHandler
    {

        public static void CheckAuthorization(ClaimsPrincipal user, object resource, IAuthorizationService _authorizationService)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(user, resource, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Access Forbidden");
            }
        }
    }
}
