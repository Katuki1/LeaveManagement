using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTest.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypes()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Sick"
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 90,
                    Name = "Test Maternity"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

            return mockRepo;

        }


    }
}
