using App.Services.Turnaments.Common.Dtos;
using Microsoft.AspNetCore.SignalR.Client;

namespace App.Web.Services.WebsocketService;

public class RegistrationWebsocketService : BaseWebsocketService, IRegistrationWebsocketService
{
    private readonly IWebsocketConnection _websocketConnection;

    private readonly List<IDisposable> _subscriptions = new();

    public RegistrationWebsocketService(IWebsocketConnection websocketConnection) : base(websocketConnection)
    {
        this._websocketConnection = websocketConnection;

        this._websocketConnection.Subscribe(nameof(RegistrationWebsocketService));
    }

    protected override void ConfigureSubscriptions()
    {
        this._subscriptions.AddRange(new[]
        {
            this.Connection.On<object>("registered", data => Registered?.Invoke(data))
        });
    }

    public async ValueTask DisposeAsync()
    {
        await this._websocketConnection.Unsubscribe(nameof(RegistrationWebsocketService));

        foreach (var subscription in this._subscriptions)
        {
            subscription.Dispose();
        }
    }

    public event Action<object>? Registered;
}

public interface IRegistrationWebsocketService : IAsyncDisposable
{
    event Action<object>? Registered;
}