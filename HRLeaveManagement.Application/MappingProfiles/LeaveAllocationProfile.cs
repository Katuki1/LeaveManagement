using AutoMapper;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationById;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HRLeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationByIdDto>();
            CreateMap<LeaveAllocation, LeaveAllocation>();
            CreateMap<LeaveAllocation, LeaveAllocation>();
        }
    }
}
