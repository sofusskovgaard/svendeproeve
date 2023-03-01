using App.Infrastructure.Events;

namespace App.Services.Authentication.Infrastructure.Events;

public class UserPasswordChangedEventMessage : IEventMessage
{
    public string UserId { get; set; }
}