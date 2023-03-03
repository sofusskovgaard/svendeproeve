using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Departments.Data.Entities;
using App.Services.Departments.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Departments.Infrastructure.CommandHandlers
{
    public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;
        public CreateDepartmentCommandHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<CreateDepartmentCommandMessage> context)
        {
            var message = context.Message;

            DepartmentEntity department = new DepartmentEntity()
            {
                Name = message.Name,
                Address = message.Address,
            };

            await _entityDataService.Create<DepartmentEntity>(department);
        }
    }
}
