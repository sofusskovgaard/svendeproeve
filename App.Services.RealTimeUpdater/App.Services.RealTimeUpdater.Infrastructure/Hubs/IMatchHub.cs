using App.Services.Turnaments.Common.Dtos;

namespace App.Services.RealTimeUpdater.Infrastructure.Hubs
{
    public interface IMatchHub
    {
        Task SendMatchUpdate(string matchId, MatchDto match);
    }
}