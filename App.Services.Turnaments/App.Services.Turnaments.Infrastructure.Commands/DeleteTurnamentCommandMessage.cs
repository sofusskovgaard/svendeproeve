using App.Infrastructure.Commands;

namespace App.Services.Turnaments.Infrastructure.Commands
{
    public class DeleteTurnamentCommandMessage : ICommandMessage
    {
        public string Id { get; set; }
    }
}
