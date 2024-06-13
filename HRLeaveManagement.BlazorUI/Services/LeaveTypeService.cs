using AutoMapper;
using HRLeaveManagement.BlazorUI.Contracts;
using HRLeaveManagement.BlazorUI.Models.LeaveTypes;
using HRLeaveManagement.BlazorUI.Services.Base;

namespace HRLeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper mapper;

        public LeaveTypeService(IClient client, IMapper mapper) : base(client)
        {
            this.mapper = mapper;
        }

        public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
        {
            try
            {
                var createCommand = mapper.Map<CreateLeaveTypeCommand>(leaveType);
                await client.LeaveTypesPOSTAsync(createCommand);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
            
        }

        public async Task<Response<Guid>> DeleteLeaveType(int id)
        {
            try
            {
                await client.LeaveRequestsDELETEAsync(id);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeById(int id)
        {
            
            var leaveType = await client.LeaveTypesGETAsync(id);
            return mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypes = await client.LeaveTypesAllAsync();
            return mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                var updateCommand = mapper.Map<UpdateLeaveTypeCommand>(leaveType);
                await client.LeaveTypesPUTAsync(id.ToString() ,updateCommand);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }

}
