using System.Security.Claims;
using App.Services.Gateway.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;

namespace App.Services.RealTimeUpdater.Infrastructure.Hubs;

public class MainHub : Hub
{
    public IHubContext<MainHub> HubContext { get; }

    private IRedisCache _redisCache;

    public MainHub(IHubContext<MainHub> context, IRedisCache redisCache)
    {
        this.HubContext = context;
        this._redisCache = redisCache;
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.User?.Identity?.IsAuthenticated ?? false)
        {
            await this._redisCache.AddSetList("main", "connections", Context.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (Context.User?.Identity?.IsAuthenticated ?? false)
        {
            await this._redisCache.RemoveSetList("main", "connections", Context.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
        }
    }

    public async Task Subscribe(string service)
    {
        await this._context.Groups.AddToGroupAsync(Context.ConnectionId, service);
    }

    public async Task Unsubscribe(string service)
    {
        await this._context.Groups.RemoveFromGroupAsync(Context.ConnectionId, service);
    }
}

public interface IMainHub
{
    IHubContext<MainHub> HubContext { get; }
}