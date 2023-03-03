using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Turnaments.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using App.Services.Gateway.Common;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetTurnamentByIdGrpcCommandResult> GetTournamentById(string id)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTurnamentByIdGrpcCommandResult>();
        }

        public async Task<GetTurnamentByMatchIdGrpcCommandResult> GetTournamentByMatch(string match)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/{match}/match");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTurnamentByMatchIdGrpcCommandResult>();
        }

        public async Task<GetTurnamentsByEventIdGrpcCommandResult> GetTournamentsByEvent(string @event)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/{@event}/event");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTurnamentsByEventIdGrpcCommandResult>();
        }

        public async Task<GetTurnamentsByGameIdGrpcCommandResult> GetTournamentsByGame(string game)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/{game}/game");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTurnamentsByGameIdGrpcCommandResult>();
        }

        public async Task<GetAllTurnamentsGrpcCommandResult> GetAllTournaments()
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

        public async Task<GetMatchesByTurnamentIdGrpcCommandResult> GetMatchesByTournament(string turnament)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/turnaments/matches/{turnament}/turnament");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetMatchesByTurnamentIdGrpcCommandResult>();
        }

        public async Task<CreateTurnamentGrpcCommandResult> CreateTournament(CreateTournamentModel data)
        {
            var request = _createRequestMessage(HttpMethod.Post, "api/turnaments");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateTurnamentGrpcCommandResult>();
        }

        public async Task<CreateMatchGrpcCommandResult> CreateMatch(CreateMatchModel data)
        {
            var request = _createRequestMessage(HttpMethod.Post, "api/turnaments/matches");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateMatchGrpcCommandResult>();
        }

        public async Task<UpdateTurnamentGrpcCommandResult> UpdateTournament(string id, UpdateTournamentModel data)
        {
            var request = _createRequestMessage(HttpMethod.Put, $"api/turnaments/{id}");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateTurnamentGrpcCommandResult>();
        }

        public async Task<UpdateMatchGrpcCommandResult> UpdateMatch(string id, UpdateMatchModel data)
        {
            var request = _createRequestMessage(HttpMethod.Put, $"api/turnaments/matches/{id}");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateMatchGrpcCommandResult>();
        }

        public async Task<DeleteTurnamentByIdGrpcCommandResult> DeleteTournament(string id)
        {
            var request = _createRequestMessage(HttpMethod.Delete, $"api/turnaments/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteTurnamentByIdGrpcCommandResult>();
        }

        public async Task<DeleteMatchByIdGrpcCommandResult> DeleteMatch(string id)
        {
            var request = _createRequestMessage(HttpMethod.Delete, $"api/turnaments/matches/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteMatchByIdGrpcCommandResult>();
        }
    }


    public partial interface IApiService
    {
        Task<GetTurnamentByIdGrpcCommandResult> GetTournamentById(string id);

        Task<GetTurnamentByMatchIdGrpcCommandResult> GetTournamentByMatch(string match);

        Task<GetTurnamentsByEventIdGrpcCommandResult> GetTournamentsByEvent(string @event);

        Task<GetTurnamentsByGameIdGrpcCommandResult> GetTournamentsByGame(string game);

        Task<GetAllTurnamentsGrpcCommandResult> GetAllTournaments();

        Task<GetMatchByIdGrpcCommandResult> GetMatchById(string id);

        Task<GetMatchesByTeamIdGrpcCommandResult> GetMatchesByTeam(string team);

        Task<GetMatchesByTurnamentIdGrpcCommandResult> GetMatchesByTournament(string turnament);

        Task<CreateTurnamentGrpcCommandResult> CreateTournament(CreateTournamentModel data);

        Task<CreateMatchGrpcCommandResult> CreateMatch(CreateMatchModel data);

        Task<UpdateTurnamentGrpcCommandResult> UpdateTournament(string id, UpdateTournamentModel data);

        Task<UpdateMatchGrpcCommandResult> UpdateMatch(string id, UpdateMatchModel data);

        Task<DeleteTurnamentByIdGrpcCommandResult> DeleteTournament(string id);

        Task<DeleteMatchByIdGrpcCommandResult> DeleteMatch(string id);
    }
}
