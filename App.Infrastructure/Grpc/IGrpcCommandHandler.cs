using App.Common.Grpc;

namespace App.Infrastructure.Grpc;

public interface IGrpcCommandHandler<TGrpcCommandMessage, TGrpcCommandResult> : IGrpcCommandHandler
    where TGrpcCommandMessage : GrpcCommandMessage
    where TGrpcCommandResult : IGrpcCommandResult
{
    Task<TGrpcCommandResult> ExecuteAsync(TGrpcCommandMessage command);
}

public interface IGrpcCommandHandler
{
}