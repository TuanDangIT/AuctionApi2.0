using AuctionApi.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AuctionApi.Authorization
{
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ReasourceOperation { get; }
        public ResourceOperationRequirement(ResourceOperation reasourceOperation)
        {
            ReasourceOperation = reasourceOperation;    
        }
    }
}
