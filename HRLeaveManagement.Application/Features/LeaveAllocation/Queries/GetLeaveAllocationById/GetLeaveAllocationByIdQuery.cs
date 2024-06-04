using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationById
{
    public class GetLeaveAllocationByIdQuery : IRequest<LeaveAllocationByIdDto>
    {
        public int Id { get; set; }
    }
}
