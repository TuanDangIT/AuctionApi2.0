using AuctionApi.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuctionApi.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Auction>
    { 
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Auction resource)
        {
            if(requirement.ReasourceOperation == ResourceOperation.Read || requirement.ReasourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if(resource.Id == userId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

}