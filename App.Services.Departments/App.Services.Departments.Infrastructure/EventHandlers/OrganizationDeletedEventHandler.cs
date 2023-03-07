using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Departments.Data.Entities;
using App.Services.Organizations.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Departments.Infrastructure.EventHandlers;

public class OrganizationDeletedEventHandler : IEventHandler<OrganizationDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public OrganizationDeletedEventHandler(IEntityDataService entityDataService)
    {
        this._entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<OrganizationDeletedEventMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<DepartmentEntity>(
            filter => filter.AnyEq(entity => entity.OrganizationIds, message.Id),
            builder => builder.Pull(entity => entity.OrganizationIds, message.Id));
    }
}