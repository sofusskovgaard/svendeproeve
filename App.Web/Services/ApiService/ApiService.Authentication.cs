using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace App.Web.Services.ApiService;

public partial class ApiService
{
    public async Task<LoginGrpcCommandResult> Login(LoginGrpcCommandMessage data)
    {
        var request = _createRequestMessage(HttpMethod.Post, "api/auth/login");
        request.Content = JsonContent.Create(data);

        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<LoginGrpcCommandResult>();
    }

    public async Task<RegisterGrpcCommandResult> Register(RegisterGrpcCommandMessage data)
    {
        var request = _createRequestMessage(HttpMethod.Post, "api/auth/register");
        request.Content = JsonContent.Create(data);

        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<RegisterGrpcCommandResult>();
    }

    public async Task<GetUserByIdGrpcCommandResult> GetCurrentlyLoggedInUser()
    {
        var request = _createRequestMessage(HttpMethod.Get, "api/auth/me", true);

        var response = await _client.SendAsync(request);

        return await response.Content.ReadFromJsonAsync<GetUserByIdGrpcCommandResult>();
    }
}

public partial interface IApiService
{
    Task<LoginGrpcCommandResult> Login(LoginGrpcCommandMessage data);

    Task<RegisterGrpcCommandResult> Register(RegisterGrpcCommandMessage data);

    Task<GetUserByIdGrpcCommandResult> GetCurrentlyLoggedInUser();
}