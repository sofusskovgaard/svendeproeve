using App.Infrastructure.Events;

namespace App.Services.Organizations.Infrastructure.Events;

public class OrganizationDeletedEventMessage : IEventMessage
{
    public string Id { get; set; }
}