using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Games.Infrastructure.Grpc;

[Service("app.services.games")]
public interface IGamesGrpcService
{
    [Operation]
    ValueTask<CreateGameGrpcCommandResult> CreateGame(CreateGameGrpcCommandMessage message);

    [Operation]
    ValueTask<GetGamesGrpcCommandResult> GetGames(GetGamesGrpcCommandMessage message);

    [Operation]
    ValueTask<GetGameByIdGrpcCommandResult> GetGameById(GetGameByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<UpdateGameGrpcCommandResult> UpdateGame(UpdateGameGrpcCommandMessage message);

    [Operation]
    ValueTask<DeleteGameByIdGrpcCommandResult> DeleteGameById(DeleteGameByIdGrpcCommandMessage message);
}