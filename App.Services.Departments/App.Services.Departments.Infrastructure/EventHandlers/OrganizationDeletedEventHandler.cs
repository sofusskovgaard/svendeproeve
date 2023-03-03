using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Departments.Data.Entities;
using App.Services.Organizations.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Departments.Infrastructure.EventHandlers
{
    public class OrganizationDeletedEventHandler : IEventHandler<OrganizationDeletedEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public OrganizationDeletedEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<OrganizationDeletedEventMessage> context)
        {
            var message = context.Message;

            var departments = await _entityDataService.ListEntities<DepartmentEntity>(filter => filter.AnyStringIn(entity => entity.OrganizationIds, message.Id));

            foreach (var department in departments)
            {
                department.OrganizationIds = department.OrganizationIds.Where(o => o != message.Id).ToArray();

                var updateDefinition = new UpdateDefinitionBuilder<DepartmentEntity>().Set(entity => entity.OrganizationIds, department.OrganizationIds);

                await _entityDataService.Update<DepartmentEntity>(filter => filter.Eq(entity => entity.Id, department.Id), _ => updateDefinition);
            }
        }
    }
}
