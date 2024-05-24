using HRLeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain;

public class LeaveType : BaseEntity
{

    public int DefaultDays { get; set; }
    public string Name { get; set; } = string.Empty;
}
