using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Games.Data.Entities;
using App.Services.Games.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Games.Infrastructure.CommandHandlers
{
    public class CreateGameCommandHandler : ICommandHandler<CreateGameCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public CreateGameCommandHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<CreateGameCommandMessage> context)
        {
            var message = context.Message;

            GameEntity game = new GameEntity
            {
                Name = message.Name,
                Discription = message.Discription,
                ProfilePicture = message.ProfilePicture,
                CoverPicture = message.CoverPicture,
                Genre = message.Genre
            };

            await _entityDataService.Create<GameEntity>(game);
        }
    }
}
