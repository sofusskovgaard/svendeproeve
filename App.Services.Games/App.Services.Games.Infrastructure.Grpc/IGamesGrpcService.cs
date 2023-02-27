using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Games.Infrastructure.Grpc
{
    [Service("app.services.games")]
    public interface IGamesGrpcService
    {
        [Operation]
        ValueTask<CreateGameCommandResult> CreateGame(CreateGameCommandMessage message);

        [Operation]
        ValueTask<GetAllGamesCommandResult> GetAllGames(GetAllGamesCommandMessage message);

        [Operation]
        ValueTask<GetGamesByNameCommandResult> GetGamesByName(GetGamesByNameCommandMessage message);
    }
}