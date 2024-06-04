using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocations(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocations();
    Task<List<LeaveAllocation>> GetLeaveAllocations(string userId);
    Task<bool> AllocationExists (string userId, int leaveTypeId, int period);
    Task AddAllocations (List<LeaveAllocation> allocations);
    Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);
}

