using App.Services.Billing.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetBillingByIdGrpcCommandResult> GetBillingById(string id)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/billing/{id}", true);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetBillingByIdGrpcCommandResult>();
        }
    }
    public partial interface IApiService
    {
        Task<GetBillingByIdGrpcCommandResult> GetBillingById(string id);
    }
}
