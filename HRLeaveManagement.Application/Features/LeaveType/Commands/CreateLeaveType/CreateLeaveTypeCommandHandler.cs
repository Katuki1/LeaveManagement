using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;


namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository repository;
        private readonly IMapper mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data

            //map from dto to data object
            var leaveType = mapper.Map<Domain.LeaveType>(request);

            //post to db thru repo
            await repository.CreateAsync(leaveType);

            //return id;
            return leaveType.Id;

        }
    }
}
