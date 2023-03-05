using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Games.Data.Entities;
using App.Services.Games.Infrastructure.Commands;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Games.Infrastructure.CommandHandlers
{
    public class UpdateGameCommandHandler : ICommandHandler<UpdateGameCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public UpdateGameCommandHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<UpdateGameCommandMessage> context)
        {
            var message = context.Message;

            var game = await _entityDataService.GetEntity<GameEntity>(message.Id);

            var updateDefinition = new UpdateDefinitionBuilder<GameEntity>().Set(entity => entity.Name, message.Name);

            if (game.Discription != message.Discription)
            {
                updateDefinition = updateDefinition.Set(entity => entity.Discription, message.Discription);
            }
            if (game.ProfilePicture != message.ProfilePicture)
            {
                updateDefinition = updateDefinition.Set(entity => entity.ProfilePicture, message.ProfilePicture);
            }
            if (game.CoverPicture != message.CoverPicture)
            {
                updateDefinition = updateDefinition.Set(entity => entity.CoverPicture, message.CoverPicture);
            }
            if (game.Genre != message.Genre)
            {
                updateDefinition = updateDefinition.Set(entity => entity.Genre, message.Genre);
            }

            await _entityDataService.Update<GameEntity>(filter => filter.Eq(entity => entity.Id, message.Id), _ => updateDefinition);
        }
    }
}
