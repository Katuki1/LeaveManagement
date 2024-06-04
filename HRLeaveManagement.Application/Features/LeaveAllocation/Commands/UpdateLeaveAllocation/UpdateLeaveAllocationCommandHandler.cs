using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocation.Command.CreateLeaveAllocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository allocationRepository;
        private readonly ILeaveTypeRepository typeRepository;
        private readonly IMapper mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository allocationRepository, 
            ILeaveTypeRepository typeRepository, IMapper mapper)
        {
            this.allocationRepository = allocationRepository;
            this.typeRepository = typeRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(typeRepository, allocationRepository);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Any())
                throw new BadRequestException("Invalid leave allocation request ", validatorResult);

            //Get leave type for allocation
            var leaveAllocation = await allocationRepository.GetByIdAsync(request.Id);

            //Assign allocations
            if (leaveAllocation is null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            mapper.Map(request, leaveAllocation);

            await allocationRepository.UpdateAsync(leaveAllocation);
            return Unit.Value;

        }
    }
}
