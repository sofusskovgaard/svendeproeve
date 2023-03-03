using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Gateway.Common;
using App.Services.Users.Common.Dtos;
using App.Web.Services;

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
        var response = await _apiService.Login(new LoginGrpcCommandMessage() { Username = username, Password = password });

        if (response.Metadata.Success)
        {
            _tokenStore.WriteToken(response.AccessToken, response.RefreshToken, DateTime.UtcNow.AddSeconds(response.ExpiresIn - 30));

            var currentUserResponse = await _apiService.GetCurrentlyLoggedInUser();
            this.CurrentUser = currentUserResponse.User;

            this.LoggedIn = true;
        }
    }

    public async Task<bool> Register(string firstname, string lastname, string username, string email, string password,
        string confirmPassword)
    {
        var response =
            await _apiService.Register(new RegisterGrpcCommandMessage()
            {
                Firstname = firstname,
                Lastname = lastname,
                Username = username,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            });

        if (response.Metadata.Success)
        {
            Console.WriteLine("SUCCESS!!");
        }

        return response.Metadata.Success;
    }

    public async Task Logout()
    {
        _tokenStore.ClearTokens();

        this.CurrentUser = null;

        this.LoggedIn = false;
    }

    //public async Task Logout()
    //{

    //}

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

        
        if (!string.IsNullOrEmpty(_tokenStore.AccessToken))
        {
            Console.WriteLine("Found AccessToken");

            if (!string.IsNullOrEmpty(_tokenStore.RefreshToken))
            {
                Console.WriteLine("Found RefreshToken");

                var response = await _apiService.GetCurrentlyLoggedInUser();
                this.CurrentUser = response.User;

                this.LoggedIn = true;
            }
        }

        this.Loaded = true;
    }

    #endregion
}

public interface ISessionStore
{
    UserDto CurrentUser { get; }

    bool LoggedIn { get; }

    bool Loaded { get; }

    Task Login(string username, string password);

    Task<bool> Register(string firstname, string lastname, string username, string email, string password,
        string confirmPassword);

    Task Logout();

    event Action? OnSessionChanged;

    event Action? OnSessionStoreLoaded;

    ValueTask InitializeAsync();
}