using App.Infrastructure.Commands;

namespace App.Services.Authentication.Infrastructure.Commands;

public class ChangeUsernameCommandMessage : ICommandMessage
{
    public string UserId { get; set; }

    public string Username { get; set; }
}