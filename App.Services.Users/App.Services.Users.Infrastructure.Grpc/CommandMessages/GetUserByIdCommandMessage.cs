using App.Infrastructure.Grpc;

namespace App.Services.Users.Infrastructure.Grpc.CommandMessages;

public class GetUserByIdCommandMessage : IGrpcCommandMessage
{
    public string Id { get; set; }
}