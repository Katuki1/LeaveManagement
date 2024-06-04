using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;

        public ChangeLeaveRequestApprovalCommandHandler(ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository, IMapper mapper, 
            IEmailSender emailSender)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            leaveRequest.Approved = request.Approved;
            await leaveRequestRepository.UpdateAsync(leaveRequest);

            //if request is approved, get and update the employee's allocation

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
