using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using App.Web.Services.ApiService;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using Microsoft.JSInterop;
using App.Services.Gateway.Common;

namespace App.Web.Stores;

public class TokenStore : ITokenStore
{
    private readonly ISyncLocalStorageService _localStorageService;

    private readonly IJSRuntime _jsruntime;

    private readonly HttpClient _httpClient;

    #region Constants

    private const string REFRESH_TOKEN = "refresh_token";

    private const string ACCESS_TOKEN = "access_token";

    private const string EXPIRE_TIME = "expire_time";

    #endregion

    public TokenStore(ISyncLocalStorageService localStorageService, IJSRuntime jsruntime, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        this._jsruntime = jsruntime;
        this._httpClient = httpClient;

        _initialize();
    }

    #region Private Fields

    public string? RefreshToken { get; private set; }

    public string? AccessToken { get; private set; }

    public DateTime? ExpireTime { get; private set; } = null;

    #endregion

    public async ValueTask<JwtPayload?> SetTokens(string accessToken, string refreshToken, int expiresIn)
    {
        var payload = await _tryGetPayload(accessToken);

        if (payload != null)
        {
            this.AccessToken = accessToken;
            this._localStorageService.SetItem(ACCESS_TOKEN, this.AccessToken);

            this.RefreshToken = refreshToken;
            this._localStorageService.SetItem(REFRESH_TOKEN, this.RefreshToken);

            this.ExpireTime = DateTime.UtcNow.AddSeconds(expiresIn-30);
            this._localStorageService.SetItem(EXPIRE_TIME, this.ExpireTime);
        }

        return payload;
    }

    public async ValueTask<string?> GetAccessToken()
    {
        if (this.ExpireTime.HasValue && DateTime.UtcNow - this.ExpireTime.Value >= TimeSpan.Zero)
        {
            await this._refreshToken();
        }

        return AccessToken;
    }

    public ValueTask<JwtPayload?> GetPayload()
    {
        return _tryGetPayload(this.AccessToken);
    }

    public void Clear()
    {
        _localStorageService.RemoveItems(new[]
        {
            ACCESS_TOKEN,
            REFRESH_TOKEN,
            EXPIRE_TIME
        });
    }

    private async ValueTask<JwtPayload?> _tryGetPayload(string token)
    {
        if (string.IsNullOrEmpty(token)) return null;

        try
        {
            return await _getPayload(token);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            try
            {
                await this._refreshToken();
                return await _getPayload(token);
            }
            catch
            {
                return null;
            }
        }
    }

    private ValueTask<JwtPayload> _getPayload(string token) => this._jsruntime.InvokeAsync<JwtPayload>("verify", new object?[] { token });

    private async Task _refreshToken()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/refresh");
        request.Content = JsonContent.Create(new RefreshTokenModel(RefreshToken));

        var rawResponse = await _httpClient.SendAsync(request);
        var response = await rawResponse.Content.ReadFromJsonAsync<RefreshTokenGrpcCommandResult>();

        if (!rawResponse.IsSuccessStatusCode)
        {
            this.Clear();
            throw new HttpRequestException(rawResponse.ReasonPhrase);
        }

        var validation = await this.GetPayload();
        if (validation == null)
        {
            this.Clear();
            return;
        }

        this.AccessToken = response.AccessToken;
        this.ExpireTime = DateTime.UtcNow.AddSeconds(response.ExpiresIn - 30);
    }

    private void _initialize()
    {
        AccessToken = _localStorageService.GetItem<string>(ACCESS_TOKEN);
        RefreshToken = _localStorageService.GetItem<string>(REFRESH_TOKEN);
        ExpireTime = _localStorageService.GetItem<DateTime?>(EXPIRE_TIME);
    }

    public class JwtPayload
    {
        public string aud { get; set; }

        public string email { get; set; }

        public int exp { get; set; }

        public int iat { get; set; }

        public bool? isAdmin { get; set; }

        public string iss { get; set; }

        public int nbf { get; set; }

        public string sub { get; set; }
    }
}

public interface ITokenStore
{
    //string? RefreshToken { get; }

    //string? AccessToken { get; }

    //DateTime? ExpireTime { get; }

    ValueTask<TokenStore.JwtPayload?> SetTokens(string accessToken, string refreshToken, int expiresIn);

    ValueTask<string?> GetAccessToken();

    ValueTask<TokenStore.JwtPayload?> GetPayload();

    void Clear();

    //Task<TokenStore.JwtPayload?> Validate(string token);

    //void WriteToken(string? accessToken = null, string? refreshToken = null, DateTime? expireTime = null);

    //void ClearTokens();
}