using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Teams.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetAllTeamsGrpcCommandMessage : IGrpcCommandMessage
    {

    }
}
