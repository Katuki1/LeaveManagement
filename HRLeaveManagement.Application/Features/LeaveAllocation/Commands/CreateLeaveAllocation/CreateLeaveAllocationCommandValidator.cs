using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandValidator :
        AbstractValidator<CreateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository repository;

        public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository repository)
        {
            this.repository = repository;


            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");

        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            var leaveType = await repository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
