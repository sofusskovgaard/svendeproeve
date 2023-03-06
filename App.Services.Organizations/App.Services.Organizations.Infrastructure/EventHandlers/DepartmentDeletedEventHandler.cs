using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Departments.Infrastructure.Events;
using App.Services.Organizations.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Organizations.Infrastructure.EventHandlers;

public class DepartmentDeletedEventHandler : IEventHandler<DepartmentDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public DepartmentDeletedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<DepartmentDeletedEventMessage> context)
    {
        var organizations = await _entityDataService.ListEntities<OrganizationEntity>(filter =>
            filter.Eq(entity => entity.DepartmentId, context.Message.Id));

        foreach (var organization in organizations)
        {
            var updateDefinition =
                new UpdateDefinitionBuilder<OrganizationEntity>().Set(entity => entity.DepartmentId, null);

            await _entityDataService.Update<OrganizationEntity>(
                filter => filter.Eq(entity => entity.Id, organization.Id), _ => updateDefinition);
        }
    }
}