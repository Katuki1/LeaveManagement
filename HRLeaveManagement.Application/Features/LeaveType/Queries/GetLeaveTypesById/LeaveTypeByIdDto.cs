using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypesById
{
    public class LeaveTypeByIdDto
    {
        public int Id { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int DefaultDays { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
