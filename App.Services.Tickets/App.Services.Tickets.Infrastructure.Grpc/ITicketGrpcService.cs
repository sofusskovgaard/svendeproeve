using App.Services.Tickets.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Tickets.Infrastructure.Grpc
{
    [Service("app.services.tickets")]
    public interface ITicketGrpcService
    {
        [Operation]
        public ValueTask<GetTicketByIdGrpcCommandResult> GetTicketById(GetTicketByIdGrpcCommandMessage message);
        [Operation]
        public ValueTask<BookTicketsGrpcCommandResult> BookTickets(BookTicketsGrpcCommandMessage message);


    }
}