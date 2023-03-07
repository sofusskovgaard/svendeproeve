using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Turnaments.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Turnaments.Infrastructure.Grpc;

[Service("app.services.turnaments")]
public interface ITurnamentsGrpcService
{
    #region Turnaments

    [Operation]
    ValueTask<GetAllTurnamentsGrpcCommandResult> GetAllTurnaments(GetAllTurnamentsGrpcCommandMessage message);

    [Operation]
    ValueTask<GetTurnamentsByEventIdGrpcCommandResult> GetTurnamentsByEventId(
        GetTurnamentsByEventIdGrpcCommandMessage message);

    [Operation]
    ValueTask<GetTurnamentsByGameIdGrpcCommandResult> GetTurnamentsByGameId(
        GetTurnamentsByGameIdGrpcCommandMessage message);

    [Operation]
    ValueTask<GetTurnamentByMatchIdGrpcCommandResult> GetTurnamentByMatchId(
        GetTurnamentByMatchIdGrpcCommandMessage message);

    [Operation]
    ValueTask<GetTurnamentByIdGrpcCommandResult> GetTuenamentById(GetTurnamentByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<CreateTurnamentGrpcCommandResult> CreateTurnament(CreateTurnamentGrpcCommandMessage message);

    [Operation]
    ValueTask<UpdateTurnamentGrpcCommandResult> UpdateTurnament(UpdateTurnamentGrpcCommandMessage message);

    [Operation]
    ValueTask<DeleteTurnamentByIdGrpcCommandResult> DeleteTurnamentById(DeleteTurnamentByIdGrpcCommandMessage message);

    #endregion

    #region Mathes

    [Operation]
    ValueTask<GetMatchesByTurnamentIdGrpcCommandResult> GetMatchesByTurnamentId(
        GetMatchesByTurnamentIdGrpcCommandMessage message);

    [Operation]
    ValueTask<GetMatchesByTeamIdGrpcCommandResult> GetMatchesByTeamId(GetMatchesByTeamIdGrpcCommandMessage message);

    [Operation]
    ValueTask<GetMatchByIdGrpcCommandResult> GetMatchById(GetMatchByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<CreateMatchGrpcCommandResult> CreateMatch(CreateMatchGrpcCommandMessage message);

    [Operation]
    ValueTask<UpdateMatchGrpcCommandResult> UpdateMatch(UpdateMatchGrpcCommandMessage message);

    [Operation]
    ValueTask<DeleteMatchByIdGrpcCommandResult> DeleteMatchById(DeleteMatchByIdGrpcCommandMessage message);

    #endregion
}