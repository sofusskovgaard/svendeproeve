using App.Infrastructure.Commands;

namespace App.Services.Games.Infrastructure.Commands;

public class CreateGameCommandMessage : ICommandMessage
{
    public string Name { get; set; }

    public string Discription { get; set; }

    public string ProfilePicture { get; set; }

    public string CoverPicture { get; set; }

    public string[] Genre { get; set; }
}