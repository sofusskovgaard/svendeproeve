using App.Infrastructure.Commands;

namespace App.Services.Turnaments.Infrastructure.Commands
{
    public class CreateMatchCommandMessage : ICommandMessage
    {
        public string Name { get; set; }
        public string[] TeamsId { get; set; }
        public string TurnamentId { get; set; }
    }
}
