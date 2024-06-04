using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;


namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IEmailSender emailSender;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            IEmailSender emailSender)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.emailSender = emailSender;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            leaveRequest.Cancelled = true;

            //Re-evaluate the employees allocations for the leave type

            //send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty, //Get email from employee record
                Subject = "Leave Request cancelled",
                Body = $"Your leave request for {request.StartDate: D} to {request.EndDate: D} " +
                        $"has been cancelled successfully. "
            };

            await emailSender.SendEmail(email);
            return Unit.Value;

        }
    }
}
