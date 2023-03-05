namespace App.Services.Gateway.Common;

public record BookTicketsModel(TicketOrderModel[] TicketOrderModels);

public record TicketOrderModel(string ProductId, string Recipient);