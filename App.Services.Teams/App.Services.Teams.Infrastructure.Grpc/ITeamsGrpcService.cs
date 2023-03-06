using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Teams.Infrastructure.Grpc;

[Service("app.services.teams")]
public interface ITeamsGrpcService
{
    [Operation]
    ValueTask<GetAllTeamsGrpcCommandResult> GetAllTeams(GetAllTeamsGrpcCommandMessage message);

    [Operation]
    ValueTask<GetTeamByIdGrpcCommandResult> GetTeamById(GetTeamByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<CreateTeamGrpcCommandResult> CreateTeam(CreateTeamGrpcCommandMessage message);

    [Operation]
    ValueTask<DeleteTeamByIdGrpcCommandResult> DeleteTeamById(DeleteTeamByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<UpdateTeamGrpcCommandResult> UpdateTeam(UpdateTeamGrpcCommandMessage message);
}