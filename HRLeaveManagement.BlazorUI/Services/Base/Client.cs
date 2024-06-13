namespace HRLeaveManagement.BlazorUI.Services.Base
{
    public partial class Client : IClient
    {
        public HttpClient httpClient 
        {
            get
            {
                return httpClient;
            }
        }
    }


}
