using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequest.Validator;
using HRLeaveManagement.Application.Logging;
using HRLeaveManagement.Application.Models.Email;
using MediatR;


namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> logger;

        public CreateLeaveRequestCommandHandler(ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
            IEmailSender emailSender, IAppLogger<CreateLeaveRequestCommandHandler> logger)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.logger = logger;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid leave request ", validationResult);

            // get requesting employee id

            // check on employee's allocation

            //if allocations aren't enough, return validation error with message

            //create leave request
            var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
            await leaveRequestRepository.CreateAsync(leaveRequest);

            try
            {
                //send confirmation email
                var email = new EmailMessage
                {
                    To = string.Empty, //Get email from employee record
                    Subject = "Leave Request Submitted",
                    Body = $"Your leave request for {request.StartDate: D} to {request.EndDate: D} " +
                            $"has been submitted successfully. "
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
