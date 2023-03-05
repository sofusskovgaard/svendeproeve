using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Teams.Data.Entities;
using App.Services.Teams.Infrastructure.Commands;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Teams.Infrastructure.CommandHandlers
{
    public class UpdateTeamCommandHandler : ICommandHandler<UpdateTeamCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public UpdateTeamCommandHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<UpdateTeamCommandMessage> context)
        {
            var message = context.Message;

            var team = await _entityDataService.GetEntity<TeamEntity>(message.TeamId);

            var updateDefinition = new UpdateDefinitionBuilder<TeamEntity>().Set(entity => entity.Name, message.Name);

            if (message.Bio != team.Bio)
            {
                updateDefinition = updateDefinition.Set(entity => entity.Bio, message.Bio);
            }
            if (message.ProfilePicturePath != team.ProfilePicturePath)
            {
                updateDefinition = updateDefinition.Set(entity => entity.ProfilePicturePath, message.ProfilePicturePath);
            }
            if (message.CoverPicturePath != team.CoverPicturePath)
            {
                updateDefinition = updateDefinition.Set(entity => entity.CoverPicturePath, message.CoverPicturePath);
            }

            await _entityDataService.Update<TeamEntity>(filter => filter.Eq(entity => entity.Id, message.TeamId), _ => updateDefinition);
        }
    }
}
