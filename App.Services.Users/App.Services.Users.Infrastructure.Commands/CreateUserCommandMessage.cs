using App.Infrastructure.Commands;

namespace App.Services.Users.Infrastructure.Commands;

public class CreateUserCommandMessage : ICommandMessage
{
    public string? Id { get; set; } = null;

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string? ConnectionId { get; set; }
}