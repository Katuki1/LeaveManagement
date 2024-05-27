using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace HRLeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfigurations : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Annual Leave",
                    DefaultDays = 21,
                    PostedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            );
        }
    }
}
