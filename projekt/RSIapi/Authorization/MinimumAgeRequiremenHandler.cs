using Microsoft.AspNetCore.Authorization;

namespace RSIapi.Authorization
{
    public class MinimumAgeRequiremenHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {

            // we can use logger to log the request try, use context .User to get the user info
            var age = context.User.FindFirst(c => c.Type == "Age").Value;
            if (age == null)
            {
                return Task.CompletedTask;
            }

            if (int.Parse(age) >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
