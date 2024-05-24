using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository repository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //fetch model from db
            var leaveType = await repository.GetByIdAsync(request.Id);

            // Validate it exists
            if(leaveType == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            //remove from db
            await repository.DeleteAsync(leaveType);

            //return void
            return Unit.Value;
            
        }
    }
}
