using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Games.Infrastructure.Grpc
{
    [Service("app.services.games")]
    public interface IGamesGrpcService
    {
        [Operation]
        ValueTask<CreateGameGrpcCommandResult> CreateGame(CreateGameGrpcCommandMessage message);

        [Operation]
        ValueTask<GetAllGamesGrpcCommandResult> GetAllGames(GetAllGamesGrpcCommandMessage message);

        [Operation]
        ValueTask<GetGamesByNameGrpcCommandResult> GetGamesByName(GetGamesByNameGrpcCommandMessage message);

        [Operation]
        ValueTask<GetGamesByGenreGrpcCommandResult> GetGamesByGenre(GetGamesByGenreGrpcCommandMessage message);

        [Operation]
        ValueTask<GetGameByIdGrpcCommandResult> GetGameById(GetGameByIdGrpcCommandMessage message);

        [Operation]
        ValueTask<UpdateGameGrpcCommandResult> updateGame(UpdateGameGrpcCommandMessage message);

        [Operation]
        ValueTask<DeleteGameByIdGrpcCommandResult> DeleteGameById(DeleteGameByIdGrpcCommandMessage message);
    }
}