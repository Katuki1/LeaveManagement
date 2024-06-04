using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler :
        IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository repository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository repository,
            IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid leave allocation request ", validatorResult);
            }

            //Get leave type for allocation
            var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            //Get Employees

            //Get Period

            //Assign allocations
            var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);
            await repository.CreateAsync(leaveAllocation);
            return Unit.Value;

        }
    }
}
