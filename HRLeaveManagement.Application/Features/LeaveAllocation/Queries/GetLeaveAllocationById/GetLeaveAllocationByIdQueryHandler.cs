using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationById
{
    public class GetLeaveAllocationByIdQueryHandler : IRequestHandler<GetLeaveAllocationByIdQuery, LeaveAllocationByIdDto>
    {
        private readonly ILeaveAllocationRepository repository;
        private readonly IMapper mapper;

        public GetLeaveAllocationByIdQueryHandler(ILeaveAllocationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<LeaveAllocationByIdDto> Handle(GetLeaveAllocationByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await repository.GetLeaveAllocations(request.Id);
            return mapper.Map<LeaveAllocationByIdDto>(leaveAllocation);

        }
    }
}
