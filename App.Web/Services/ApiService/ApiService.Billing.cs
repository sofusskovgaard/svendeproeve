using App.Services.Billing.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        //public async Task<GetChargeByOrderGrpcCommandResult> GetBillingById(string id)
        //{
        //    var request = await _createRequestMessage(HttpMethod.Get, $"api/billing/{id}");

        //    var response = await _client.SendAsync(request);
        //    return await response.Content.ReadFromJsonAsync<GetChargeByOrderGrpcCommandResult>();
        //}
    }
    public partial interface IApiService
    {
        //Task<GetChargeByOrderGrpcCommandResult> GetBillingById(string id);
    }
}
