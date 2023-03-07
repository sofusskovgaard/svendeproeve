using App.Common.Grpc;
using App.Services.Billing.Common.Dtos;
using ProtoBuf;

namespace App.Services.Billing.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetCardsForUserGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public UserCardDto[] Data { get; set; }
}