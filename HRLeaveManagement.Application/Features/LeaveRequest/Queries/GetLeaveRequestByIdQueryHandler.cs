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
    public class GetLeaveRequestByIdQueryHandler : IRequestHandler<GetLeaveRequestByIdQuery, LeaveRequestByIdDto>
    {
        private readonly ILeaveRequestRepository repository;
        private readonly IMapper mapper;

        public GetLeaveRequestByIdQueryHandler(ILeaveRequestRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<LeaveRequestByIdDto> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = mapper.Map<LeaveRequestByIdDto>(await
               repository.GetLeaveRequestsWithDetails(request.Id));

            //add employee details as needed

            return leaveRequest;
        }
    }
}
