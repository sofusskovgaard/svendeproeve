﻿using App.Infrastructure.Events;
using App.Services.RealTimeUpdater.Infrastructure.Hubs;
using App.Services.Tournaments.Common.Dtos;
using App.Services.Tournaments.Infrastructure.Events;
using AutoMapper;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.RealTimeUpdater.Infrastructure.EventHandlers
{
    public class MatchUpdatedEventHandler : IEventHandler<MatchUpdatedEventMessage>
    {
        private readonly IMatchHub _matchHub;

        public MatchUpdatedEventHandler(IMatchHub matchHub)
        {
            _matchHub = matchHub;
        }

        public async Task Consume(ConsumeContext<MatchUpdatedEventMessage> context)
        {
            var message = context.Message;

            var match = new MatchDto
            {
                Id = message.Id,
                Name = message.Name,
                TeamsId = message.TeamsId,
                TournamentId = message.TournamentId,
                WinningTeamId = message.WinningTeamId
            };

            await _matchHub.SendMatchUpdate(message.Id, match);
        }
    }
}
