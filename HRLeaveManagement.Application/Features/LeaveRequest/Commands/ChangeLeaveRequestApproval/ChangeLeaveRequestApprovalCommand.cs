using HRLeaveManagement.Application.Features.LeaveRequest.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommand : BaseLeaveRequest, IRequest<Unit>
    {
        public int Id { get; set; }
        public bool Approved { get; set; }
    }
}
