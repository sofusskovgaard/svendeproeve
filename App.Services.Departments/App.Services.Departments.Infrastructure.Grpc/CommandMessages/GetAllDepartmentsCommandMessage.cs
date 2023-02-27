using App.Infrastructure.Grpc;
using ProtoBuf;

namespace App.Services.Departments.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class GetAllDepartmentsCommandMessage : IGrpcCommandMessage
    {

    }
}
