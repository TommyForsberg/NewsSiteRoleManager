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
        //If the claim is not present then the user is of an evaluated role and age should not be checked.
        if (!context.User.HasClaim(c => c.Type == CustomClaimTypes.MinimumAge))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var age =  int.Parse(context.User.FindFirst(c => c.Type == CustomClaimTypes.MinimumAge).Value);


        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}