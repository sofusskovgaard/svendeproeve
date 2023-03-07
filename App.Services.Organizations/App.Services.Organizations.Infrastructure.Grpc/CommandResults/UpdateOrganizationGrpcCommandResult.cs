﻿using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Organizations.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class UpdateOrganizationGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}