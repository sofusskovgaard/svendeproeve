using App.Services.Tickets.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetTicketByIdGrpcCommandResult> GetTicketById(string id)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/tickets/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTicketByIdGrpcCommandResult>();
        }

        public async Task<BookTicketsGrpcCommandResult> BookTickets(BookTicketsGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Post, $"api/tickets");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<BookTicketsGrpcCommandResult>();
        }

    }

    public partial interface IApiService
    {
        Task<GetTicketByIdGrpcCommandResult> GetTicketById(string id);

        Task<BookTicketsGrpcCommandResult> BookTickets(BookTicketsGrpcCommandMessage data);

    }
}
