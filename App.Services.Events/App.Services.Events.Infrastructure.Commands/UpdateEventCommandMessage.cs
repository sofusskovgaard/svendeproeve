using App.Infrastructure.Commands;

namespace App.Services.Events.Infrastructure.Commands
{
    public class UpdateEventCommandMessage : ICommandMessage
    {
        public string Id { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
