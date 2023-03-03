using App.Infrastructure.Commands;

namespace App.Services.Turnaments.Infrastructure.Commands
{
    public class CreateTurnamentCommandMessage : ICommandMessage
    {
        public string Name { get; set; }
        public string GameId { get; set; }
        public string EventId { get; set; }
    }
}
