using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using App.Services.Gateway.Common;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetTeamByIdGrpcCommandResult> GetTeamById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/teams/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetTeamByIdGrpcCommandResult>();
        }

        public async Task<CreateTeamGrpcCommandResult> CreateTeam(CreateTeamModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Post, "api/teams");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateTeamGrpcCommandResult>();
        }

        public async Task<UpdateTeamGrpcCommandResult> Updateteam(string id, UpdateTeamModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Put, "api/teams");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateTeamGrpcCommandResult>();
        }

        public async Task<DeleteTeamByIdGrpcCommandResult> DeleteTeam(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Delete, "api/teams");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteTeamByIdGrpcCommandResult>();
        }
    }

    public partial interface IApiService
    {
        Task<GetTeamByIdGrpcCommandResult> GetTeamById(string id);

        Task<CreateTeamGrpcCommandResult> CreateTeam(CreateTeamModel data);

        Task<UpdateTeamGrpcCommandResult> Updateteam(string id, UpdateTeamModel data);

        Task<DeleteTeamByIdGrpcCommandResult> DeleteTeam(string id);

    }
}
