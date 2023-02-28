using App.Infrastructure.Commands;

namespace App.Services.Authentication.Infrastructure.Commands;

public class ChangePasswordCommandMessage : ICommandMessage
{
    public string UserId { get; set; }
    public string Password { get; set; }
}