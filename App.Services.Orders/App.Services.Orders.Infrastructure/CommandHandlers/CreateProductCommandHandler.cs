using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Orders.Data.Entities;
using App.Services.Orders.Infrastructure.Commands;
using App.Services.Orders.Infrastructure.Events;
using MassTransit;

namespace App.Services.Orders.Infrastructure.CommandHandlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommandMessage>
    {
        public IEntityDataService _entityDataService { get; set; }

        public IPublishEndpoint _publishEndpoint { get; set; }

        public CreateProductCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CreateProductCommandMessage> context)
        {
            var message = context.Message;

            var entity = new ProductEntity
            {
                Description = message.Description,
                Name = message.Name,
                Price = message.Price,
            };

            await _entityDataService.SaveEntity(entity);

            var eventMessage = new ProductCreatedEventMessage
            {
                Id = entity.Id
            };

            await _publishEndpoint.Publish(eventMessage);
        }
    }
}
