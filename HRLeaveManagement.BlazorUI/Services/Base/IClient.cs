namespace HRLeaveManagement.BlazorUI.Services.Base
{
    public partial interface IClient
    {
        public HttpClient httpClient { get; }
    }
}
