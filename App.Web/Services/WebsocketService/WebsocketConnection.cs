using System.Reflection.Metadata.Ecma335;
using App.Web.Stores;
using Microsoft.AspNetCore.SignalR.Client;

namespace App.Web.Services.WebsocketService;

public class WebsocketConnection : IWebsocketConnection
{
    public WebsocketConnection(ITokenStore tokenStore)
    {
        this.Connection = new HubConnectionBuilder()
            .WithUrl($"https://localhost:3001/main", options =>
            {
                options.AccessTokenProvider = async () => await tokenStore.GetAccessToken();
                //options.HttpMessageHandlerFactory = handler => new HttpClientHandler()
                //{
                //    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
                //};
            })
            .WithAutomaticReconnect()
            .Build();

        this.Connection.StartAsync();
    }

    public HubConnection Connection { get; }

    public async ValueTask DisposeAsync()
    {
        await this.Connection.DisposeAsync();
    }
}

public interface IWebsocketConnection : IAsyncDisposable
{
    public HubConnection Connection { get; }
}