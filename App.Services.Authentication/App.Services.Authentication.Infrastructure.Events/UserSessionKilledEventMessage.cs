using App.Infrastructure.Events;

namespace App.Services.Authentication.Infrastructure.Events;

public class UserSessionKilledEventMessage : IEventMessage
{
    public string UserId { get; set; }

    public string SessionId { get; set; }
}