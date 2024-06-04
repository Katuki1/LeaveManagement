using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequest.Validator;
using HRLeaveManagement.Application.Logging;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> logger;

        public UpdateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
            IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> logger)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            var validator = new UpdateLeaveRequestCommandValidator(
                leaveTypeRepository, leaveRequestRepository);

            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid leave request ", validationResult);

            mapper.Map(request, leaveRequest);

            await leaveRequestRepository.UpdateAsync(leaveRequest);

            try
            {
                //send confirmation email
                var email = new EmailMessage
                {
                    To = string.Empty, //Get email from employee record
                    Subject = "Leave Request Submitted",
                    Body = $"Your leave request for {request.StartDate: D} to {request.EndDate: D} " +
                            $"has been updated successfully. "
                };

                await emailSender.SendEmail(email);
            } 
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
            }

            return Unit.Value;

        }
    }
}
