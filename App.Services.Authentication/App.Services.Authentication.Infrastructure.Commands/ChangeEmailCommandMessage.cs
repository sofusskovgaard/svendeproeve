using App.Infrastructure.Commands;
using App.Infrastructure.Events;

namespace App.Services.Authentication.Infrastructure.Commands;

public class ChangeEmailCommandMessage : ICommandMessage
{
    public string UserId { get; set; }

    public string Email { get; set; }
}