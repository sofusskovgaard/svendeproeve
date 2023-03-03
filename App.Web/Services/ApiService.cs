using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using App.Web.Stores;

namespace App.Web.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _client;

    private readonly ITokenStore _tokenStore;

    public ApiService(HttpClient client, ITokenStore tokenStore)
    {
        _client = client;
        _tokenStore = tokenStore;
    }

    public async Task<LoginGrpcCommandResult> Login(LoginGrpcCommandMessage data)
    {
        var content = JsonContent.Create(data);
        var response = await _client.PostAsync("api/auth/login", content);
        return await response.Content.ReadFromJsonAsync<LoginGrpcCommandResult>();
    }

    public async Task<RegisterGrpcCommandResult> Register(RegisterGrpcCommandMessage data)
    {
        var content = JsonContent.Create(data);
        var response = await _client.PostAsync("api/auth/register", content);
        return await response.Content.ReadFromJsonAsync<RegisterGrpcCommandResult>();
    }

    public async Task<GetUserByIdGrpcCommandResult> GetCurrentlyLoggedInUser()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/auth/me");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.AccessToken);
        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<GetUserByIdGrpcCommandResult>();
    }

    public async Task<GetUserByIdGrpcCommandResult> GetUserById(string id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/users/{id}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.AccessToken);
        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<GetUserByIdGrpcCommandResult>();
    }
}

public interface IApiService
{
    Task<LoginGrpcCommandResult> Login(LoginGrpcCommandMessage data);

    Task<RegisterGrpcCommandResult> Register(RegisterGrpcCommandMessage data);

    Task<GetUserByIdGrpcCommandResult> GetCurrentlyLoggedInUser();

    Task<GetUserByIdGrpcCommandResult> GetUserById(string id);

    //Task<GrpcCommandResult> Me();
}
