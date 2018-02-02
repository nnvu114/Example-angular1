using System;
using System.Collections.Generic;
using Petronas.SampleApp.Model.NotMapping;
using Microsoft.AspNet.Identity;

namespace Petronas.SampleApp.Model.Migrations
{
    public static class BuildUsers
    {
        public static List<ApplicationUser> Build()
        {
            var passwordHash = new PasswordHasher();
            var password = passwordHash.HashPassword("Test@123");

            return new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "admin",
                    PasswordHash = password,
                    PhoneNumber = "0987888999",
                    FirstName = "Super",
                    LastName = "Administrator",
                    Email = "admin@Petronas.SampleApp.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ExpireDate = DateTime.Now.AddDays(90)
                }
            };
        }
    }
}