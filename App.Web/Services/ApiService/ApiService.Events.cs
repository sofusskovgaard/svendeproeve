using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Events.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using App.Services.Gateway.Common;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetEventsGrpcCommandResult> GetAllEvents()
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/events");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetEventsGrpcCommandResult>();
        }

        public async Task<GetEventByIdGrpcCommandResult> GetEventById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/events/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetEventByIdGrpcCommandResult>();
        }

        public async Task<CreateEventGrpcCommandResult> CreateEvent(CreateEventModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Post, "api/events");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateEventGrpcCommandResult>();
        }
    }

    public partial interface IApiService 
    {
        Task<GetEventsGrpcCommandResult> GetAllEvents();

        Task<GetEventByIdGrpcCommandResult> GetEventById(string id);

        Task<CreateEventGrpcCommandResult> CreateEvent(CreateEventModel data);
    }
}
