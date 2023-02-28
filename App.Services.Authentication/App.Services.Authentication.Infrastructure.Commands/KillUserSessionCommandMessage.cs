using App.Infrastructure.Commands;

namespace App.Services.Authentication.Infrastructure.Commands;

public class KillUserSessionCommandMessage : ICommandMessage
{
    public string UserId { get; set; }

    public string SessionId { get; set; }
}