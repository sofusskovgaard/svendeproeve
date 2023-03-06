using App.Infrastructure.Commands;

namespace App.Services.Teams.Infrastructure.Commands;

public class UpdateTeamCommandMessage : ICommandMessage
{
    public string TeamId { get; set; }

    public string Name { get; set; }

    public string Bio { get; set; }

    public string ProfilePicturePath { get; set; }

    public string CoverPicturePath { get; set; }
}