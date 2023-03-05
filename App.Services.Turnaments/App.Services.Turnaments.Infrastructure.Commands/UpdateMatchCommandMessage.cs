using App.Infrastructure.Commands;

namespace App.Services.Turnaments.Infrastructure.Commands
{
    public class UpdateMatchCommandMessage : ICommandMessage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string WinningTeamId { get; set; }
    }
}
