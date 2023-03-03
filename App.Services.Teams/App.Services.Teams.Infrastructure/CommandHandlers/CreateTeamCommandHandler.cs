using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Teams.Data.Entities;
using App.Services.Teams.Infrastructure.Commands;
using App.Services.Teams.Infrastructure.Events;
using MassTransit;
using Microsoft.IdentityModel.Tokens;

namespace App.Services.Teams.Infrastructure.CommandHandlers
{
    public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateTeamCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CreateTeamCommandMessage> context)
        {
            var message = context.Message;

            TeamEntity team = new TeamEntity()
            {
                Name = message.Name,
                Bio = message.Bio,
                ProfilePicturePath = message.ProfilePicturePath,
                CoverPicturePath = message.CoverPicturePath,
                GameId = message.GameId,
                OrganizationId = message.OrganizationId,
                MembersId = message.MembersId,
                ManagerId = message.ManagerId,
            };

            team = await _entityDataService.Create<TeamEntity>(team);

            List<string> users = new List<string>();

            if (!team.MembersId.IsNullOrEmpty())
            {
                users = team.MembersId.ToList();
            }
            if (!team.MembersId.Contains(team.ManagerId) && !string.IsNullOrEmpty(team.ManagerId))
            {
                users.Add(team.ManagerId);
            }

            TeamCreatedEventMessage eventMessage = new TeamCreatedEventMessage()
            {
                Id = team.Id,
                OrganizationId = team.OrganizationId,
                UsersId = users.ToArray()
            };

            await _publishEndpoint.Publish(eventMessage);
        }
    }
}
