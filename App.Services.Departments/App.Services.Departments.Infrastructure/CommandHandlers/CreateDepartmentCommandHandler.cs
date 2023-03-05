using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Departments.Infrastructure.CommandHandlers;

public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public CreateDepartmentCommandHandler(IEntityDataService entityDataService)
    {
        this._entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<CreateDepartmentCommandMessage> context)
    {
        var message = context.Message;

        var department = new DepartmentEntity
        {
            Name = message.Name,
            Address = message.Address
        };

        await this._entityDataService.Create(department);
    }
}