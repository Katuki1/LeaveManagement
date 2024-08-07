﻿using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : 
        AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository repository;
        private readonly ILeaveAllocationRepository allocationRepository;

        public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository repository, 
            ILeaveAllocationRepository allocationRepository)
        {
            this.repository = repository;
            this.allocationRepository = allocationRepository;


            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.") ;

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveAllocationMustExist)
                .WithMessage("{PropertyName} must be present.");



        }

        private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token)
        {
            var leaveAllocation = await allocationRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            var leaveType = await repository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
