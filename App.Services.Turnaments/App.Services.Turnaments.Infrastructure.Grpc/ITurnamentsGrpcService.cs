using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Turnaments.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Turnaments.Infrastructure.Grpc
{
    [Service("app.services.turnaments")]
    public interface ITurnamentsGrpcService
    {
        [Operation]
        ValueTask<GetAllTurnamentsCommandResult> GetAllTurnaments(GetAllTurnamentsCommandMessage message);

        [Operation]
        ValueTask<GetTurnamentsByEventIdCommandResult> GetTurnamentsByEventId(GetTurnamentsByEventIdCommandMessage message);

        [Operation]
        ValueTask<GetTurnamentsByGameIdCommandResult> GetTurnamentsByGameId(GetTurnamentsByGameIdCommandMessage message);

        [Operation]
        ValueTask<GetTurnamentByMatchIdCommandResult> GetTurnamentByMatchId(GetTurnamentByMatchIdCommandMessage message);

        [Operation]
        ValueTask<GetTurnamentByIdCommandResult> GetTuenamentById(GetTurnamentByIdCommandMessage message);

        [Operation]
        ValueTask<CreateTurnamentCommandResult> CreateTurnament(CreateTurnamentCommandMessage message);

        [Operation]
        ValueTask<UpdateTurnamentCommandResult> UpdateTurnament(UpdateTurnamentCommandMessage message);
    }
}