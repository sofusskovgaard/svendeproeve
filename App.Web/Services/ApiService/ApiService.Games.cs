using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;
using App.Services.Gateway.Common;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetGameByIdGrpcCommandResult> GetGameById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Get, $"api/games/{id}");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetGameByIdGrpcCommandResult>();
        }

        public async Task<GetGamesGrpcCommandResult> GetAllGames()
        {
            var request = await _createRequestMessage(HttpMethod.Get, "api/games");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetGamesGrpcCommandResult>();
        }

        public async Task<CreateGameGrpcCommandResult> CreateGame(CreateGameModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Post, "api/games");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateGameGrpcCommandResult>();
        }

        public async Task<UpdateGameGrpcCommandResult> UpdateGame(string id, UpdateGameModel data)
        {
            var request = await _createRequestMessage(HttpMethod.Put, $"api/games/{id}");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateGameGrpcCommandResult>();
        }

        public async Task<DeleteGameByIdGrpcCommandResult> DeleteGameById(string id)
        {
            var request = await _createRequestMessage(HttpMethod.Delete, $"api/games/{id}");
            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteGameByIdGrpcCommandResult>();
        }
    }

    public partial interface IApiService
    {
        Task<GetGameByIdGrpcCommandResult> GetGameById(string id);

        Task<GetGamesGrpcCommandResult> GetAllGames();

        Task<CreateGameGrpcCommandResult> CreateGame(CreateGameModel data);

        Task<UpdateGameGrpcCommandResult> UpdateGame(string id, UpdateGameModel data);

        Task<DeleteGameByIdGrpcCommandResult> DeleteGameById(string id);

    }
}
