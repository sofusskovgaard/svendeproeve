using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using App.Services.Gateway.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.Web.Services.ApiService;

public partial class ApiService
{
    public async Task<LoginGrpcCommandResult> Login(LoginModel data)
    {
        var request = await _createRequestMessage(HttpMethod.Post, "api/auth/login");
        request.Content = JsonContent.Create(data);

        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<LoginGrpcCommandResult>();
    }

    public async Task<RegisterGrpcCommandResult> Register(RegisterModel data)
    {
        var request = await _createRequestMessage(HttpMethod.Post, "api/auth/register");
        request.Content = JsonContent.Create(data);

        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<RegisterGrpcCommandResult>();
    }

    public async Task<RefreshTokenGrpcCommandResult> Refresh(string refreshToken)
    {
        var request = await _createRequestMessage(HttpMethod.Post, "api/auth/register", true);
        request.Content = JsonContent.Create(new RefreshTokenModel(refreshToken));

        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<RefreshTokenGrpcCommandResult>();
    }

    public async Task<GetUserByIdGrpcCommandResult> GetCurrentlyLoggedInUser()
    {
        var request = await _createRequestMessage(HttpMethod.Get, "api/auth/me");

        var response = await _client.SendAsync(request);

        return await response.Content.ReadFromJsonAsync<GetUserByIdGrpcCommandResult>();
    }

    public async Task<GetPublicKeyGrpcCommandResult> GetPublicKey()
    {
        var request = await _createRequestMessage(HttpMethod.Get, "api/auth/public-key");
        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<GetPublicKeyGrpcCommandResult>();   
    }
}

public partial interface IApiService
{
    Task<LoginGrpcCommandResult> Login(LoginModel data);

    Task<RegisterGrpcCommandResult> Register(RegisterModel data);

    Task<RefreshTokenGrpcCommandResult> Refresh(string refreshToken);

    Task<GetUserByIdGrpcCommandResult> GetCurrentlyLoggedInUser();

    Task<GetPublicKeyGrpcCommandResult> GetPublicKey();
}