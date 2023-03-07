using App.Services.Tournaments.Common.Dtos;
using Microsoft.AspNetCore.SignalR.Client;

namespace App.Web.Services.WebsocketService;

public abstract class BaseWebsocketService
{
    private readonly IWebsocketConnection _websocketConnection;

    protected BaseWebsocketService(IWebsocketConnection websocketConnection)
    {
        this._websocketConnection = websocketConnection;

        this.ConfigureSubscriptions();
    }

    protected HubConnection Connection => _websocketConnection.Connection;

    protected virtual void ConfigureSubscriptions() { }
}