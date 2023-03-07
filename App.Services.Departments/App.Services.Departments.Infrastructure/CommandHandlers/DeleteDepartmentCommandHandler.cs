using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Commands;
using App.Services.Departments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Departments.Infrastructure.CommandHandlers;

public class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteDepartmentCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        this._entityDataService = entityDataService;
        this._publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeleteDepartmentCommandMessage> context)
    {
        var message = context.Message;

        var result = await this._entityDataService.Delete<DepartmentEntity>(filter => filter.Eq(entity => entity.Id, message.Id));

        if (result)
        {
            await this._publishEndpoint.Publish(new DepartmentDeletedEventMessage{ Id = message.Id });
        }
    }
}