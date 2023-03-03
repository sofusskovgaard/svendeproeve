using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using App.Services.Gateway.Common;
using App.Web.Stores;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public async Task<LoginResponse> Login(LoginModel data)
    {
        var content = JsonContent.Create(data);
        var response = await _client.PostAsync("api/auth/login", content);
        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<RegisterResponse> Register(RegisterModel data)
    {
        var content = JsonContent.Create(data);
        var response = await _client.PostAsync("api/auth/register", content);
        return await response.Content.ReadFromJsonAsync<RegisterResponse>();
    }

    public async Task<MeResponse> Me()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/auth/me");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.AccessToken);
        var response = await _client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<MeResponse>();
    }
}

public interface IApiService
{
    Task<LoginResponse> Login(LoginModel data);

    Task<RegisterResponse> Register(RegisterModel data);

    Task<MeResponse> Me();
}

public abstract record BaseResponse()
{
    public ResponseMetadata? Metadata { get; set; }
}

public record ResponseMetadata(bool Success, string? Message = null, string[]? Errors = null);

#region Authentication

public record LoginResponse(string AccessToken, string RefreshToken, string Type, int ExpiresIn) : BaseResponse;

public record RegisterResponse(string Id, string Username, string Email) : BaseResponse;

public record MeResponse(UserDto User) : BaseResponse;

public record UserDto(string Id, string Firstname, string Lastname, string Username, string Email);

#endregion