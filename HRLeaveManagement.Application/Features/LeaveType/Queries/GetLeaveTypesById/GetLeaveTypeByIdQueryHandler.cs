using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypesById
{
    public class GetLeaveTypeByIdQueryHandler : IRequestHandler<GetLeaveTypeByIdQuery, LeaveTypeByIdDto>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository repository;

        public GetLeaveTypeByIdQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<LeaveTypeByIdDto> Handle(GetLeaveTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveType = await repository.GetByIdAsync(request.id);

            // Validate it exists
            if (leaveType == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.id);
            }

            var data = mapper.Map<LeaveTypeByIdDto>(leaveType);

            return data;


        }
    }
}
