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
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "92f8f9d7-2330-4754-a6bf-abcef65f27bf",
                    UserId = "3a6c9140-e4e2-46f5-ad40-174f292f6a5d"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "4d549acb-7b2e-42db-90ed-4a9b58cda3ee",
                    UserId = "2c1e9de7-4d05-44ab-82f2-94c539efbf23"
                }
             );
        }
    }
}
