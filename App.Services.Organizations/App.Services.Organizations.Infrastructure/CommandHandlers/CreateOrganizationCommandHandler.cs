using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Organizations.Data.Entities;
using App.Services.Organizations.Infrastructure.Commands;
using App.Services.Organizations.Infrastructure.Events;
using MassTransit;

namespace App.Services.Organizations.Infrastructure.CommandHandlers
{
    public class CreateOrganizationCommandHandler : ICommandHandler<CreateOrganizationCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IPublishEndpoint _publishEndpoint;

        public CreateOrganizationCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<CreateOrganizationCommandMessage> context)
        {
            var message = context.Message;

            var entity = new OrganizationEntity
            {
                Address = message.Address,
                Bio = message.Bio,
                CoverPicture = message.CoverPicture,
                Name = message.Name,
                ProfilePicture = message.ProfilePicture,
                DepartmentId = message.DepartmentId,
            };

            await _entityDataService.SaveEntity(entity);

            await _publishEndpoint.Publish(new OrganizationCreatedEventMessage { Id = entity.Id, DepartmentId = entity.DepartmentId });
        }
    }
}
