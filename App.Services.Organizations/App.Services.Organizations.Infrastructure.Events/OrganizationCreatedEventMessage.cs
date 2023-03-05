using App.Infrastructure.Events;

namespace App.Services.Organizations.Infrastructure.Events
{
    public class OrganizationCreatedEventMessage : IEventMessage
    {
        public string Id { get; set; }
        public string DepartmentId { get; set; }
    }
}
