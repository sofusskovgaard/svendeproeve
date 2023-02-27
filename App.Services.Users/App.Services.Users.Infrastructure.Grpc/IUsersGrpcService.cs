﻿using ProtoBuf.Grpc.Configuration;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;

namespace App.Services.Users.Infrastructure.Grpc;

[Service("app.services.users")]
public interface IUsersGrpcService
{
    [Operation]
    ValueTask<GetUserByIdGrpcCommandResult> GetUserById(GetUserByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<GetUsersGrpcCommandResult> GetUsers(GetUsersGrpcCommandMessage message);

    [Operation]
    ValueTask<GetUsersInTeamGrpcCommandResult> GetUsersInTeam(GetUsersInTeamGrpcCommandMessage message);

    [Operation]
    ValueTask<GetUsersInOrganizationGrpcCommandResult> GetUsersInOrganization(GetUsersInOrganizationGrpcCommandMessage message);

    [Operation]
    ValueTask<CreateUserGrpcCommandResult> CreateUser(CreateUserGrpcCommandMessage message);

    [Operation]
    ValueTask<TestCommandResult> Test();
}