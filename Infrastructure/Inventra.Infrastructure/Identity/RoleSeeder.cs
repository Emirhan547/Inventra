using Inventra.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Identity
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<AppRole> roleManager)
        {
            var roles = new[]
            {
            Roles.Admin,
            Roles.Manager,
            Roles.Employee
        };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new AppRole
                        {
                            Name = role
                        });
                }
            }
        }
    }
}
    
