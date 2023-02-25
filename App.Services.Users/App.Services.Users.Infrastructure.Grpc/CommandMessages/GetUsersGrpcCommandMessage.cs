using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class GetUsersGrpcCommandMessage : IGrpcCommandMessage
{
    
}