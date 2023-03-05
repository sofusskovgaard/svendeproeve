using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Events.Infrastructure.Grpc.CommandResults;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Events.Infrastructure.Grpc;

[Service("app.services.events")]
public interface IEventsGrpcService
{
    [Operation]
    ValueTask<GetEventByIdGrpcCommandResult> GetEventById(GetEventByIdGrpcCommandMessage message);

    [Operation]
    ValueTask<CreateEventGrpcCommandResult> CreateEvent(CreateEventGrpcCommandMessage message);

    [Operation]
    ValueTask<UpdateEventGrpcCommandResult> UpdateEvent(UpdateEventGrpcCommandMessage message);

    [Operation]
    ValueTask<DeleteEventGrpcCommandResult> DeleteEvent(DeleteEventGrpcCommandMessage message);

    [Operation]
    ValueTask<GetEventsGrpcCommandResult> GetEvents(GetEventsGrpcCommandMessage message);
}