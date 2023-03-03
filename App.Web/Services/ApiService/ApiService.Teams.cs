using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetTeamByIdGrpcCommandResult> GetTeamById(string id)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/teams/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTeamByIdGrpcCommandResult>();
        }

        public async Task<GetTeamsByOrganizationIdGrpcCommandResult> GetTeamByOrganization(string organization)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/teams/{organization}/organization");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTeamsByOrganizationIdGrpcCommandResult>();
        }
        public async Task<GetTeamsByNameGrpcCommandResult> GetTeamByName(string name)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/teams/{name}/name");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTeamsByNameGrpcCommandResult>();
        }
        public async Task<GetTeamsByMemberIdGrpcCommandResult> GetTeamByMember(string member)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/teams/{member}/member");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTeamsByMemberIdGrpcCommandResult>();
        }

        public async Task<GetTeamsByGameIdGrpcCommandResult> GetTeamByGame(string game)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/teams/{game}/game");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTeamsByGameIdGrpcCommandResult>();
        }

        public async Task<GetTeamsByManagerIdGrpcCommandResult> GetTeamByManager(string manager)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/teams/{manager}/manager");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTeamsByManagerIdGrpcCommandResult>();
        }

        public async Task<CreateTeamGrpcCommandResult> CreateTeam(CreateTeamGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Post, "api/teams");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateTeamGrpcCommandResult>();
        }

        public async Task<UpdateTeamGrpcCommandResult> Updateteam(UpdateTeamGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Put, "api/teams");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateTeamGrpcCommandResult>();
        }

        public async Task<DeleteTeamByIdGrpcCommandResult> DeleteTeam(string id)
        {
            var request = _createRequestMessage(HttpMethod.Delete, "api/teams");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteTeamByIdGrpcCommandResult>();
        }
    }

    public partial interface IApiService
    {
        Task<GetTeamByIdGrpcCommandResult> GetTeamById(string id);

        Task<GetTeamsByOrganizationIdGrpcCommandResult> GetTeamByOrganization(string organization);

        Task<GetTeamsByNameGrpcCommandResult> GetTeamByName(string name);

        Task<GetTeamsByMemberIdGrpcCommandResult> GetTeamByMember(string member);

        Task<GetTeamsByGameIdGrpcCommandResult> GetTeamByGame(string game);

        Task<GetTeamsByManagerIdGrpcCommandResult> GetTeamByManager(string manager);

        Task<CreateTeamGrpcCommandResult> CreateTeam(CreateTeamGrpcCommandMessage data);

        Task<UpdateTeamGrpcCommandResult> Updateteam(UpdateTeamGrpcCommandMessage data);

        Task<DeleteTeamByIdGrpcCommandResult> DeleteTeam(string id);

    }
}
