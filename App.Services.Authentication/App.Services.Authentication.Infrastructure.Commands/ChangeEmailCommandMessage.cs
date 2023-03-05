using App.Infrastructure.Commands;

namespace App.Services.Authentication.Infrastructure.Commands;

public class ChangeEmailCommandMessage : ICommandMessage
{
    public string UserId { get; set; }

    public string Email { get; set; }
}