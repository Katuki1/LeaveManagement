using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, 
        ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<LeaveRequest> GetLeaveRequestsWithDetails(int id)
        {
            var leaveRequest = await dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequest = await dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequest = await dbContext.LeaveRequests
                .Where(q => q.RequestingEmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveRequest;
        }
    }


}
