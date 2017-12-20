﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsSite.Data
{
    public class AgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
                return Task.CompletedTask;

            var dateOfBirth = DateTime.Parse(context.User.FindFirst(
                c => c.Type == ClaimTypes.DateOfBirth).Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (calculatedAge > requirement.MinimumAge)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

      
    }
}
