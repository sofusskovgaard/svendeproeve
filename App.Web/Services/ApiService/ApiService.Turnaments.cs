using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Turnaments.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetTurnamentByIdGrpcCommandResult> GetTurnamentById(string id)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTurnamentByIdGrpcCommandResult>();
        }

        public async Task<GetTurnamentByMatchIdGrpcCommandResult> GetTurnamentByMatch(string match)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/{match}/match");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTurnamentByMatchIdGrpcCommandResult>();
        }

        public async Task<GetTurnamentsByEventIdGrpcCommandResult> GetTurnamentsByEvent(string @event)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/{@event}/event");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTurnamentsByEventIdGrpcCommandResult>();
        }

        public async Task<GetTurnamentsByGameIdGrpcCommandResult> GetTurnamentsByGame(string game)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/{game}/game");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTurnamentsByGameIdGrpcCommandResult>();
        }

        public async Task<GetAllTurnamentsGrpcCommandResult> GetAllTurnaments()
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetAllTurnamentsGrpcCommandResult>();
        }

        public async Task<GetMatchByIdGrpcCommandResult> GetMatchById(string id)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/matches/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetMatchByIdGrpcCommandResult>();
        }

        public async Task<GetMatchesByTeamIdGrpcCommandResult> GetMatchesByTeam(string team)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/matches/{team}/team");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetMatchesByTeamIdGrpcCommandResult>();
        }

        public async Task<GetMatchesByTurnamentIdGrpcCommandResult> GetMatchesByTurnament(string turnament)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/matches/{turnament}/turnament");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetMatchesByTurnamentIdGrpcCommandResult>();
        }

        public async Task<CreateTurnamentGrpcCommandResult> CreateTurnament(CreateTurnamentGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Post, "api/turnaments");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateTurnamentGrpcCommandResult>();
        }

        public async Task<CreateMatchGrpcCommandResult> CreateMatch(CreateMatchGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Post, "api/turnaments/matches");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateMatchGrpcCommandResult>();
        }

        public async Task<UpdateTurnamentGrpcCommandResult> UpdateTurnament(UpdateTurnamentGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Put, "api/turnaments");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateTurnamentGrpcCommandResult>();
        }

        public async Task<UpdateMatchGrpcCommandResult> UpdateMatch(UpdateMatchGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Put, "api/turnaments/matches");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateMatchGrpcCommandResult>();
        }

        public async Task<DeleteTurnamentByIdGrpcCommandResult> DeleteTurnament(string id)
        {
            var request = _createRequestMessage(HttpMethod.Delete, "api/turnaments");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteTurnamentByIdGrpcCommandResult>();
        }

        public async Task<DeleteMatchByIdGrpcCommandResult> DeleteMatch(string id)
        {
            var request = _createRequestMessage(HttpMethod.Delete, "api/turnaments/matches");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteMatchByIdGrpcCommandResult>();
        }
    }


    public partial interface IApiService
    {
        Task<GetTurnamentByIdGrpcCommandResult> GetTurnamentById(string id);

        Task<GetTurnamentByMatchIdGrpcCommandResult> GetTurnamentByMatch(string match);

        Task<GetTurnamentsByEventIdGrpcCommandResult> GetTurnamentsByEvent(string @event);

        Task<GetTurnamentsByGameIdGrpcCommandResult> GetTurnamentsByGame(string game);

        Task<GetAllTurnamentsGrpcCommandResult> GetAllTurnaments();

        Task<GetMatchByIdGrpcCommandResult> GetMatchById(string id);

        Task<GetMatchesByTeamIdGrpcCommandResult> GetMatchesByTeam(string team);

        Task<GetMatchesByTurnamentIdGrpcCommandResult> GetMatchesByTurnament(string turnament);

        Task<CreateTurnamentGrpcCommandResult> CreateTurnament(CreateTurnamentGrpcCommandMessage data);

        Task<CreateMatchGrpcCommandResult> CreateMatch(CreateMatchGrpcCommandMessage data);

        Task<UpdateTurnamentGrpcCommandResult> UpdateTurnament(UpdateTurnamentGrpcCommandMessage data);

        Task<UpdateMatchGrpcCommandResult> UpdateMatch(UpdateMatchGrpcCommandMessage data);

        Task<DeleteTurnamentByIdGrpcCommandResult> DeleteTurnament(string id);

        Task<DeleteMatchByIdGrpcCommandResult> DeleteMatch(string id);
    }
}
