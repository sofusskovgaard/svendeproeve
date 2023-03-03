using Blazored.LocalStorage;

namespace App.Web.Stores;

public class TokenStore : ITokenStore
{
    private readonly ISyncLocalStorageService _localStorageService;

    #region Constants

    private const string REFRESH_TOKEN = "refresh_token";

    private const string ACCESS_TOKEN = "access_token";

    private const string EXPIRE_TIME = "expire_time";

    #endregion

    public TokenStore(ISyncLocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;

        _initialize();
    }

    #region Private Fields

    public string? RefreshToken { get; private set; }

    public string? AccessToken { get; private set; }

    public DateTime? ExpireTime { get; private set; }

    #endregion

    public void GetTokens()
    {
        AccessToken = _localStorageService.GetItemAsString(ACCESS_TOKEN);
        RefreshToken = _localStorageService.GetItemAsString(REFRESH_TOKEN);
        ExpireTime = _localStorageService.GetItem<DateTime>(EXPIRE_TIME);
    }

    public void WriteToken(string? accessToken = null, string? refreshToken = null, DateTime? expireTime = null)
    {
        if (!string.IsNullOrEmpty(accessToken))
        {
            AccessToken = accessToken;
            _localStorageService.SetItemAsString(ACCESS_TOKEN, accessToken ?? AccessToken);
        }

        if (!string.IsNullOrEmpty(refreshToken))
        {
            RefreshToken = refreshToken;
            _localStorageService.SetItemAsString(REFRESH_TOKEN, refreshToken ?? RefreshToken);
        }

        if (expireTime.HasValue)
        {
            ExpireTime = expireTime.Value;
            _localStorageService.SetItem(EXPIRE_TIME, expireTime ?? ExpireTime);
        }
    }

    public void ClearTokens()
    {
        _localStorageService.RemoveItems(new[]
        {
            ACCESS_TOKEN,
            REFRESH_TOKEN,
            EXPIRE_TIME
        });
    }

    private void _initialize()
    {
        AccessToken = _localStorageService.GetItemAsString(ACCESS_TOKEN);
        RefreshToken = _localStorageService.GetItemAsString(REFRESH_TOKEN);
        ExpireTime = _localStorageService.GetItem<DateTime>(EXPIRE_TIME);
    }
}

public interface ITokenStore
{
    string? RefreshToken { get; }

    string? AccessToken { get; }

    DateTime? ExpireTime { get; }

    void GetTokens();

    void WriteToken(string? accessToken = null, string? refreshToken = null, DateTime? expireTime = null);

    void ClearTokens();
}