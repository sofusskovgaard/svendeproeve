using App.Infrastructure.Commands;

namespace App.Services.Orders.Infrastructure.Commands;

public class CreateProductCommandMessage : ICommandMessage
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string? ReferenceId { get; set; }

    public string? ReferenceType { get; set; }
}