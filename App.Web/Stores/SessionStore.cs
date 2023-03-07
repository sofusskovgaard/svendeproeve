using System.IdentityModel.Tokens.Jwt;
using App.Services.Gateway.Common;
using App.Services.Users.Common.Dtos;
using App.Web.Services.ApiService;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace App.Web.Stores;

public class SessionStore : ISessionStore
{
    private readonly IApiService _apiService;

    private readonly ITokenStore _tokenStore;

    public SessionStore(IApiService apiService, ITokenStore tokenStore)
    {
        _apiService = apiService;
        _tokenStore = tokenStore;

        this.InitializeAsync();
    }

    #region Private Fields

    private bool _loggedIn;

    private bool _loaded;

    #endregion

    #region Properties

    public UserDto? CurrentUser { get; private set; }

    public bool IsAdmin { get; private set; }

    public bool LoggedIn
    {
        get => _loggedIn;

        private set
        {
            _loggedIn = value;
            _notifySessionChanged();
        }
    }

    public bool Loaded
    {
        get => _loaded;
        private set
        {
            _loaded = value;
            _notifySessionStoreLoaded();
        }
    } 

    #endregion

    #region Methods

    public async Task Login(string username, string password)
    {
        var response = await _apiService.Login(new LoginModel(username, password));

        if (response.Metadata.Success)
        {
            var payload = await this._tokenStore.SetTokens(response.Data.AccessToken, response.Data.RefreshToken, response.Data.ExpiresIn);

            if (payload != null)
            {
                this.IsAdmin = payload?.isAdmin ?? false;

                var currentUserResponse = await _apiService.GetCurrentlyLoggedInUser();
                this.CurrentUser = currentUserResponse.Data;

                this.LoggedIn = true;
            }
        }
    }

    public async Task<bool> Register(string firstname, string lastname, string username, string email, string password,
        string confirmPassword)
    {
        var response =
            await _apiService.Register(new RegisterModel(firstname, lastname, username, email, password, confirmPassword));

        if (response.Metadata.Success)
        {
            Console.WriteLine("SUCCESS!!");
        }

        return response.Metadata.Success;
    }

    public void Logout()
    {
        this._tokenStore.Clear();

        this.CurrentUser = null;

        this.LoggedIn = false;
    }

    #endregion

    #region Events

    public event Action? OnSessionStoreLoaded;

    private void _notifySessionStoreLoaded()
    {
        Console.WriteLine("Notify session store loaded");
        OnSessionStoreLoaded?.Invoke(); 
    }

    public event Action? OnSessionChanged;

    private void _notifySessionChanged()
    {
        Console.WriteLine("Notify session changed", this.LoggedIn);
        OnSessionChanged?.Invoke();
    }

    #endregion

    #region Disposal / Initialization

    public async ValueTask InitializeAsync()
    {
        Console.WriteLine("Initializing");

        var payload = await this._tokenStore.GetPayload();
        if (payload != null)
        {
            Console.WriteLine("Found tokens");

            if (payload.isAdmin ?? false)
            {
                Console.WriteLine("Data is administrator");
                this.IsAdmin = true;
            }

            var currentUserResponse = await _apiService.GetCurrentlyLoggedInUser();
            this.CurrentUser = currentUserResponse.Data;

            this.LoggedIn = true;
        }

        this.Loaded = true;
    }

    #endregion
}

public interface ISessionStore
{
    UserDto CurrentUser { get; }

    bool IsAdmin { get; }

    bool LoggedIn { get; }

    bool Loaded { get; }

    Task Login(string username, string password);

    Task<bool> Register(string firstname, string lastname, string username, string email, string password,
        string confirmPassword);

    void Logout();

    event Action? OnSessionChanged;

    event Action? OnSessionStoreLoaded;

    ValueTask InitializeAsync();
}