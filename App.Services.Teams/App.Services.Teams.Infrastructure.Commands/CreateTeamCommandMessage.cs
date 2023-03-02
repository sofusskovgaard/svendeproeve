using App.Infrastructure.Commands;

namespace App.Services.Teams.Infrastructure.Commands
{
    public class CreateTeamCommandMessage : ICommandMessage
    {
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicturePath { get; set; }
        public string? CoverPicturePath { get; set; }
        public string GameId { get; set; }
        public string[]? MembersId { get; set; }
        public string? ManagerId { get; set; }
        public string OrganizationId { get; set; }
    }
}
