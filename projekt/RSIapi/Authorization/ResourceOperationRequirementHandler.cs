using Microsoft.AspNetCore.Authorization;
using RSIapi.Models;
using System.Security.Claims;

namespace RSIapi.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Board>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Board board)
        {
            if (requirement.Operation == ResourceOperation.Read || requirement.Operation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if(board.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
