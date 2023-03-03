using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using System.Net.Http.Json;

namespace App.Web.Services.ApiService
{
    public partial class ApiService
    {
        public async Task<GetGameByIdGrpcCommandResult> GetGameById(string id)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/games/{id}", true);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetGameByIdGrpcCommandResult>();
        }

        public async Task<GetAllGamesGrpcCommandResult> GetAllGames()
        {
            var request = _createRequestMessage(HttpMethod.Get, "api/games", true);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetAllGamesGrpcCommandResult>();
        }

        public async Task<GetGamesByNameGrpcCommandResult> GetGamesByName(string name)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/games/{name}/name", true);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetGamesByNameGrpcCommandResult>();
        }

        public async Task<GetGamesByGenreGrpcCommandResult> GetGameByGenre(string genre)
        {
            var request = _createRequestMessage(HttpMethod.Get, $"api/games/{genre}/genre", true);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<GetGamesByGenreGrpcCommandResult>();
        }

        public async Task<CreateGameGrpcCommandResult> CreateGame(CreateGameGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Post, "api/games");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<CreateGameGrpcCommandResult>();
        }

        public async Task<UpdateGameGrpcCommandResult> UpdateGame(UpdateGameGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Put, "api/games");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UpdateGameGrpcCommandResult>();
        }

        public async Task<DeleteGameByIdGrpcCommandResult> DeleteGameById(DeleteGameByIdGrpcCommandMessage data)
        {
            var request = _createRequestMessage(HttpMethod.Delete, "api/games");
            request.Content = JsonContent.Create(data);

            var response = await _client.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<DeleteGameByIdGrpcCommandResult>();
        }
    }

    public partial interface IApiService
    {
        Task<GetGameByIdGrpcCommandResult> GetGameById(string id);

        Task<GetAllGamesGrpcCommandResult> GetAllGames();

        Task<GetGamesByNameGrpcCommandResult> GetGamesByName(string name);

        Task<GetGamesByGenreGrpcCommandResult> GetGameByGenre(string genre);

        Task<CreateGameGrpcCommandResult> CreateGame(CreateGameGrpcCommandMessage data);

        Task<UpdateGameGrpcCommandResult> UpdateGame(UpdateGameGrpcCommandMessage data);

        Task<DeleteGameByIdGrpcCommandResult> DeleteGameById(DeleteGameByIdGrpcCommandMessage data);

    }
}
