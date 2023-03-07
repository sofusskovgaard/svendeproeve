using App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Tournaments.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using App.Services.Gateway.Common;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetTournamentByIdGrpcCommandResult> GetTournamentById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/tournaments/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTournamentByIdGrpcCommandResult>();
        }

        public async Task<GetTournamentByMatchIdGrpcCommandResult> GetTournamentByMatch(string match)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/tournaments/{match}/match");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTournamentByMatchIdGrpcCommandResult>();
        }

        public async Task<GetTournamentsByGameIdGrpcCommandResult> GetTournamentsByGame(string game)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/tournaments/{game}/game");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTournamentsByGameIdGrpcCommandResult>();
        }

        public async Task<GetTournamentsGrpcCommandResult> GetAllTournaments()
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/tournaments");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTournamentsGrpcCommandResult>();
        }

        public async Task<GetMatchByIdGrpcCommandResult> GetMatchById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/matches/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetMatchByIdGrpcCommandResult>();
        }

        public async Task<GetMatchesByTeamIdGrpcCommandResult> GetMatchesByTeam(string team)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/tournaments/matches/{team}/team");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetMatchesByTeamIdGrpcCommandResult>();
        }

        public async Task<CreateTournamentGrpcCommandResult> CreateTournament(CreateTournamentModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Post, "api/tournaments");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateTournamentGrpcCommandResult>();
        }

        public async Task<CreateMatchGrpcCommandResult> CreateMatch(CreateMatchModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Post, "api/tournaments/matches");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateMatchGrpcCommandResult>();
        }

        public async Task<UpdateTournamentGrpcCommandResult> UpdateTournament(string id, UpdateTournamentModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Put, $"api/tournaments/{id}");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateTournamentGrpcCommandResult>();
        }

        public async Task<UpdateMatchGrpcCommandResult> UpdateMatch(string id, UpdateMatchModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Put, $"api/tournaments/matches/{id}");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateMatchGrpcCommandResult>();
        }

        public async Task<DeleteTournamentByIdGrpcCommandResult> DeleteTournament(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Delete, $"api/tournaments/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteTournamentByIdGrpcCommandResult>();
        }

        public async Task<DeleteMatchByIdGrpcCommandResult> DeleteMatch(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Delete, $"api/tournaments/matches/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteMatchByIdGrpcCommandResult>();
        }
    }


    public partial interface IApiService
    {
        Task<GetTournamentByIdGrpcCommandResult> GetTournamentById(string id);

        Task<GetTournamentByMatchIdGrpcCommandResult> GetTournamentByMatch(string match);

        Task<GetTournamentsByGameIdGrpcCommandResult> GetTournamentsByGame(string game);

        Task<GetTournamentsGrpcCommandResult> GetAllTournaments();

        Task<GetMatchByIdGrpcCommandResult> GetMatchById(string id);

        Task<GetMatchesByTeamIdGrpcCommandResult> GetMatchesByTeam(string team);

        Task<CreateTournamentGrpcCommandResult> CreateTournament(CreateTournamentModel data);

        Task<CreateMatchGrpcCommandResult> CreateMatch(CreateMatchModel data);

        Task<UpdateTournamentGrpcCommandResult> UpdateTournament(string id, UpdateTournamentModel data);

        Task<UpdateMatchGrpcCommandResult> UpdateMatch(string id, UpdateMatchModel data);

        Task<DeleteTournamentByIdGrpcCommandResult> DeleteTournament(string id);

        Task<DeleteMatchByIdGrpcCommandResult> DeleteMatch(string id);
    }
}
