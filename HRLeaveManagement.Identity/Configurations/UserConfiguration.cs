using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var harsher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "3a6c9140-e4e2-46f5-ad40-174f292f6a5d",
                    Email = "admin@localhost.com",
                    NormalizedEmail = ("admin@localhost.com").ToUpper(),
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = ("admin@localhost.com").ToUpper(),
                    PasswordHash = harsher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "2c1e9de7-4d05-44ab-82f2-94c539efbf23",
                    Email = "user@localhost.com",
                    NormalizedEmail = ("user@localhost.com").ToUpper(),
                    FirstName = "System",
                    LastName = "User",
                    UserName = "user@localhost.com",
                    NormalizedUserName = ("user@localhost.com").ToUpper(),
                    PasswordHash = harsher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                }
              );

        }
    }
}
