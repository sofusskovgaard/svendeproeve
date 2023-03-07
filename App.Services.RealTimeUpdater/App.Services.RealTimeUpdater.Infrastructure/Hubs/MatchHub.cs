using App.Services.Tournaments.Common.Dtos;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Infrastructure.Hubs
{
    public class MatchHub : Hub, IMatchHub
    {
        private IHubContext<MatchHub> _context;

        public MatchHub(IHubContext<MatchHub> context)
        {
            _context = context;
        }

        public async Task SendMatchUpdate(string matchId, MatchDto match)
        {
            await _context.Clients.All.SendAsync("match-" + matchId, match);
        }
    }
}
