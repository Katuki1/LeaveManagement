using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRLeaveManagement.Application.Features.LeaveRequest.DTOs
{
    public class LeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequested { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public bool? Approved { get; set; }
        public string RequestingEmployeeId { get; set; } = string.Empty;
    }
}
