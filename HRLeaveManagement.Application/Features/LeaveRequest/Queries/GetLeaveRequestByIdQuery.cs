using HRLeaveManagement.Application.Features.LeaveRequest.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries
{
    public class GetLeaveRequestByIdQuery : IRequest<LeaveRequestByIdDto>
    {
        public int Id { get; set; }
    }
}
