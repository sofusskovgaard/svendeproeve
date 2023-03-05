namespace App.Services.Gateway.Common;

//public record CreateOrderModel(string UserId, decimal Total, string[] TicketIds);

public record CreateProductModel(string Name, string Description, decimal Price);