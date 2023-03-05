using App.Infrastructure.Events;

namespace App.Services.Teams.Infrastructure.Events
{
    public class TeamCreatedEventMessage : IEventMessage
    {
        public string Id { get; set; }
        public string OrganizationId { get; set; }
        public string[] UsersId { get; set; }
    }
}
