using AuctionApi.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuctionApi.Authorization
{
    public class ResourceOperationRequirementHandler<T> : AuthorizationHandler<ResourceOperationRequirement, T>
    { 
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, T resource)
        {
            if(requirement.ReasourceOperation == ResourceOperation.Read || requirement.ReasourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if(resource is Auction auction)
            {
                if (auction.UserId == userId)
                {
                    context.Succeed(requirement);
                }
            }
            if(resource is User user)
            {
                if(user.Id == userId)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }

}