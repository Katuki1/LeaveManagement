using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveRequest.Validator;
using FluentValidation;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;



namespace HRLeaveManagement.Application.Features.LeaveRequest.Validator
{
    public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveRequestRepository = leaveRequestRepository;

            //Include(new BaseLeaveRequestValidator(leaveTypeRepository));
            Include(new BaseLeaveRequestValidator(leaveTypeRepository));

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .MustAsync(LeaveRequestMustExist)
                .WithMessage("{PropertyName} does not exist.");

        }

        private async Task<bool> LeaveRequestMustExist(int id, CancellationToken token)
        {
            var leaveAllocation = await leaveRequestRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }




    }
}
