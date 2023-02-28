using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Turnaments.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Turnaments.Infrastructure.Grpc
{
    [Service("app.services.turnaments")]
    public interface ITurnamentsGrpcService
    {
    }
}