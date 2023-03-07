using App.Services.Tournaments.Common.Dtos;
using Microsoft.AspNetCore.SignalR.Client;

namespace App.Web.Services.WebsocketService;

public class MatchWebsocketService : BaseWebsocketService, IMatchWebsocketService
{
    private readonly List<IDisposable> subscriptions = new();

    public MatchWebsocketService(IWebsocketConnection websocketConnection) : base(websocketConnection)
    {
    }

    protected override void ConfigureSubscriptions()
    {
        this.subscriptions.AddRange(new[]
        {
            this.Connection.On<MatchDto>("match", data => MatchChanged?.Invoke(data))
        });
    }

    public void Dispose()
    {
        foreach (var subscription in this.subscriptions)
        {
            subscription.Dispose();
        }
    }

    public event Action<MatchDto>? MatchChanged;
}

public interface IMatchWebsocketService : IDisposable
{
    event Action<MatchDto>? MatchChanged;
}