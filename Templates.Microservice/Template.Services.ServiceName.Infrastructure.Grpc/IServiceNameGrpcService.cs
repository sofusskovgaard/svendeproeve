﻿using ProtoBuf.Grpc.Configuration;
using Template.Services.ServiceName.Infrastructure.Grpc.CommandMessages;
using Template.Services.ServiceName.Infrastructure.Grpc.CommandResults;

namespace Template.Services.ServiceName.Infrastructure.Grpc;

[Service]
public interface IServiceNameGrpcService
{
    [Operation]
    ValueTask<GetByIdGrpcCommandResult> GetById(GetByIdGrpcCommandMessage message);
}