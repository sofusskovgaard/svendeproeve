using App.Infrastructure.Commands;

namespace App.Services.Organizations.Infrastructure.Commands;

public class DeleteOrganizationCommandMessage : ICommandMessage
{
    public string Id { get; set; }
}