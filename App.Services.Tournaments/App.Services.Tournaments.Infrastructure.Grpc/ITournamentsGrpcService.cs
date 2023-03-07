using App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Tournaments.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Tournaments.Infrastructure.Grpc;

[Service("app.services.tournaments")]
public interface ITournamentsGrpcService
{
    #region Tournaments

    [Operation]
    ValueTask<GetTournamentsGrpcCommandResult> GetTournaments(GetTournamentsGrpcCommandMessage message);

    [Operation]
    ValueTask<GetTournamentByIdGrpcCommandResult> GetTournamentById(GetTournamentByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<GetTournamentByMatchIdGrpcCommandResult> GetTournamentByMatchId(GetTournamentByMatchIdGrpcCommandMessage message);

    [Operation]
    ValueTask<CreateTournamentGrpcCommandResult> CreateTournament(CreateTournamentGrpcCommandMessage message);

    [Operation]
    ValueTask<UpdateTournamentGrpcCommandResult> UpdateTournament(UpdateTournamentGrpcCommandMessage message);

    [Operation]
    ValueTask<DeleteTournamentByIdGrpcCommandResult> DeleteTournamentById(DeleteTournamentByIdGrpcCommandMessage message);

    #endregion

    #region Mathes

    [Operation]
    ValueTask<GetMatchesGrpcCommandResult> GetMatches(GetMatchesGrpcCommandMessage message);

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