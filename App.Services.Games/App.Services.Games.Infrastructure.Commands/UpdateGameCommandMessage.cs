using App.Infrastructure.Commands;

namespace App.Services.Games.Infrastructure.Commands;

public class UpdateGameCommandMessage : ICommandMessage
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ProfilePicture { get; set; }

    public string CoverPicture { get; set; }

    public string[] Genre { get; set; }
}