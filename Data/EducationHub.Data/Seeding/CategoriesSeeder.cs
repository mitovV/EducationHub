﻿namespace EducationHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Common;
    using Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(EducationHubDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var user = dbContext.Users.FirstOrDefault(u => u.Email == GlobalConstants.AdministratorEmail);

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Programing Languages",
                    UserId = user.Id,
                    PictureUrl = "https://res.cloudinary.com/dgw65zfwf/image/upload/v1605711654/EducationHub/Categories/Best-Programming-Languages-to-Start-Learning-Today_tyaj6n.jpg",
                },
                new Category
                {
                    Name = "Basic Math",
                    UserId = user.Id,
                    PictureUrl = "https://res.cloudinary.com/dgw65zfwf/image/upload/v1605711703/EducationHub/Categories/maxresdefault_tvvi97.jpg",
                },
                new Category
                {
                    Name = "Geography",
                    UserId = user.Id,
                    PictureUrl = "https://res.cloudinary.com/dgw65zfwf/image/upload/v1605711726/EducationHub/Categories/geogImg_r_qshnjy.jpg",
                },
                new Category
                {
                    Name = "Chemistry",
                    UserId = user.Id,
                    PictureUrl = "https://res.cloudinary.com/dgw65zfwf/image/upload/v1605711749/EducationHub/Categories/chemistry_logo_yr5fqx.jpg",
                },
            };

            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
