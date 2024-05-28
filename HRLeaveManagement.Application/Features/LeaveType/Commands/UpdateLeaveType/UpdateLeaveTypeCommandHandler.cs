using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Logging;
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
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> logger;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repository,
            IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate data
            var validator = new UpdateLeaveTypeCommandValidator(repository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any()) 
            {
                logger.LogWarning("Validation errors in update request for {0} - {1}",
                    nameof(LeaveType), request.Id);
                throw new BadRequestException("Invalid leave type ", validationResult);
            }

            //map dto to model
            var leaveType = mapper.Map<Domain.LeaveType>(request); 

            //update in db
            await repository.UpdateAsync(leaveType);

            //return void
            return Unit.Value;

        }
    }
}
