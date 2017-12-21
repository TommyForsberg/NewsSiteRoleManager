using Microsoft.AspNetCore.Authorization;
using NewsSite.Data;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   MinimumAgeRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "AtLeast21"))
        {
            return Task.CompletedTask;
        }

        var age =  int.Parse(context.User.FindFirst(c => c.Type == "AtLeast21").Value);


        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}