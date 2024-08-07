﻿using HRLeaveManagement.Application.Features.LeaveRequest.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest <Unit>
    {
        public int Id { get; set; }
        public string RequestComments { get; set; }
        public bool Cancelled { get; set; }
    }
}
