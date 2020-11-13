﻿namespace EducationHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    public class AdminUserSeeder : ISeeder
    {
        public async Task SeedAsync(EducationHubDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any(u => u.Email == GlobalConstants.AdministratorEmail))
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var user = new User
            {
                Email = GlobalConstants.AdministratorEmail,
                UserName = GlobalConstants.AdministratorEmail,
            };

            var password = GlobalConstants.AdministratorPassword;

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
            }

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
