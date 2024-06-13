using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Identity.DbContext
{
    public class HRLeaveDbContext : IdentityDbContext<ApplicationUser>
    {
        public HRLeaveDbContext(DbContextOptions<HRLeaveDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(HRLeaveDbContext).Assembly);

        }

    }
}
