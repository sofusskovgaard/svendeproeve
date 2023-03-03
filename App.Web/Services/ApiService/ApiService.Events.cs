using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Events.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetEventByIdGrpcCommandResult> GetEventById(string id)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/events/{id}", true);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetEventByIdGrpcCommandResult>();
        }

        public async Task<CreateEventGrpcCommandResult> CreateEvent(CreateEventGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Post, "api/events");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateEventGrpcCommandResult>();
        }
    }

    public partial interface IApiService 
    {
        Task<GetEventByIdGrpcCommandResult> GetEventById(string id);
        Task<CreateEventGrpcCommandResult> CreateEvent(CreateEventGrpcCommandMessage data);
    }
}
