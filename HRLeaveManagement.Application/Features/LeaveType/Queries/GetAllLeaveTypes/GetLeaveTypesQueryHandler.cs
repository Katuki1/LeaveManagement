using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery,
        List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository repository;
        private readonly IMapper mapper;

        public GetLeaveTypesQueryHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, 
            CancellationToken cancellationToken)
        {
            // Query the db
            var leaveTypes = await repository.GetAllAsync();

            // Validate it exists
            if (leaveTypes == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            //Convert data objects to DTO objecs
            var data = mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            //Return list of DTO objects
            return data;

        }
    }
}
