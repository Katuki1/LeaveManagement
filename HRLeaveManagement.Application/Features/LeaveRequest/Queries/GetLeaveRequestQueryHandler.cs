using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveRequest.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries
{
    public class GetLeaveRequestQueryHandler : IRequestHandler<GetLeaveRequestQuery,
        List<LeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository repository;
        private readonly IMapper mapper;

        public GetLeaveRequestQueryHandler(ILeaveRequestRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            //check if employee is logged in

            var leaveRequests = await repository.GetLeaveRequestsWithDetails();
            var requests = mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            //fill requests with employee details

            return requests;

        }
    }
}
