using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, 
        ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await dbContext.LeaveTypes.AnyAsync(t => t.Name == name);
        }
    }


}
