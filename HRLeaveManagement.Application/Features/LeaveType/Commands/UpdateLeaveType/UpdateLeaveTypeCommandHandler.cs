using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository repository;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate data

            //map dto to model
            var leaveType = mapper.Map<Domain.LeaveType>(request); 

            //update in db
            await repository.UpdateAsync(leaveType);

            //return void
            return Unit.Value;

        }
    }
}
