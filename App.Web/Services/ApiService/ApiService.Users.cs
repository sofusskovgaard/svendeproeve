using App.Services.Users.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.Web.Services.ApiService;

public partial class ApiService
{
    public async Task<GetUserByIdGrpcCommandResult> GetUserById(string id)
    {
        var request = await _createRequestMessage(HttpMethod.Get, $"api/users/{id}");

        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<GetUserByIdGrpcCommandResult>();
    }
}

public partial interface IApiService
{
    Task<GetUserByIdGrpcCommandResult> GetUserById(string id);
}