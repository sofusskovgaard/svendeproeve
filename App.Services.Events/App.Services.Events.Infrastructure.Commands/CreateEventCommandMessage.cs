using App.Infrastructure.Commands;

namespace App.Services.Events.Infrastructure.Commands;

public class CreateEventCommandMessage : ICommandMessage
{
    public string EventName { get; set; }

    public string Location { get; set; }

    public string[] Tournaments { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}