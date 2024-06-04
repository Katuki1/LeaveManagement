using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationQueryHandler : IRequestHandler<GetLeaveAllocationQuery, 
        List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository repository;
        private readonly IMapper mapper;

        public GetLeaveAllocationQueryHandler(ILeaveAllocationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationQuery request, CancellationToken cancellationToken)
        {
            //Get records for specific user
            //Get allocations per employee

            var leaveAllocations = await repository.GetLeaveAllocations();
            var allocations = mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

            return allocations;
        }
    }
}
