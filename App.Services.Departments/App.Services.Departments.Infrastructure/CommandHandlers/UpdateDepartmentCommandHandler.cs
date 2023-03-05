using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Commands;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Departments.Infrastructure.CommandHandlers;

public class UpdateDepartmentCommandHandler : ICommandHandler<UpdateDepartmentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public UpdateDepartmentCommandHandler(IEntityDataService entityDataService)
    {
        this._entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<UpdateDepartmentCommandMessage> context)
    {
        var message = context.Message;

        var department =
            await this._entityDataService.GetEntity<DepartmentEntity>(filter =>
                filter.Eq(entity => entity.Id, message.Id));

        var updateDefinition = new UpdateDefinitionBuilder<DepartmentEntity>().Set(entity => entity.Name, message.Name);

        if (department.Address != message.Address)
            updateDefinition = updateDefinition.Set(entity => entity.Address, message.Address);

        await this._entityDataService.Update<DepartmentEntity>(filter => filter.Eq(entity => entity.Id, message.Id),
            _ => updateDefinition);
    }
}