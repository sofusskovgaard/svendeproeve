using App.Infrastructure.Commands;

namespace App.Services.Turnaments.Infrastructure.Commands
{
    public class UpdateTurnamentCommandMessage : ICommandMessage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GameId { get; set; }
        public string[] MatchesId { get; set; }
    }
}
